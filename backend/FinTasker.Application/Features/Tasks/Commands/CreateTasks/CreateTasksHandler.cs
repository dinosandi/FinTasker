using MediatR;
using FinTasker.Application.Common.Models;
using FinTasker.Application.Common.Interfaces.Service;
using FinTasker.Application.Features.Tasks.DTOs;
using FinTasker.Application.Common.Interfaces.Repository;
using FinTasker.Application.Common.Exceptions;



namespace FinTasker.Application.Features.Tasks.Commands.CreateTasks
{
    public class CreateTasksHandler
        : IRequestHandler<CreateTasksCommand, ApiResponse<TaskDto>>

    {
        private readonly ITasksRepository _tasksRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IProjectsService _projectsService;

        public CreateTasksHandler(ITasksRepository tasksRepository, IProjectsService projectsService, ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
            _tasksRepository = tasksRepository;
            _projectsService = projectsService;
        }

        public async Task<ApiResponse<TaskDto>> Handle(
            CreateTasksCommand request,
            CancellationToken cancellationToken)
        {
            // cek apakah user login
            var userId = _currentUserService.UserId
                ?? throw new UnauthorizedAccessException(
                    "User is not logged in.");

            // cek apakah project dengan id tersebut ada di database
            var project = await _projectsService.GetProjectsByIdAsync(request.ProjectId);

            if (project == null)
                throw new NotFoundException("Project not found.");

            // jika user tidak memiliki akses ke project tersebut
            if (project.UsersId != userId)
                throw new UnauthorizedAccessException(
                    "User does not have permission.");

            // Mapping entity
            var newTasks = new Domain.Entities.Tasks
            {

                ProjectId = request.ProjectId,
                Title = request.Title,
                Description = request.Description,
                Priority = request.Priority,
                Status = request.Status,
                DueDate = request.DueDate,
                Estimed_Minutes = request.Estimed_Minutes,
                CompletedAt = request.CompletedAt,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow,
            };

            // Save
            await _tasksRepository.CreateTasksAsync(newTasks, cancellationToken);

            // response
            var response = new TaskDto
            {
                ProjectId = newTasks.ProjectId,
                Title = newTasks.Title,
                Description = newTasks.Description,
                Priority = newTasks.Priority,
                Status = newTasks.Status,
                DueDate = newTasks.DueDate,
                Estimed_Minutes = newTasks.Estimed_Minutes,
                CompletedAt = newTasks.CompletedAt
            };
            
            return ApiResponse<TaskDto>.Created(
                response,
                "Task created successfully"
            );

        }

    }
}



