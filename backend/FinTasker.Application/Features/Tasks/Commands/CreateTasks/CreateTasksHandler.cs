using MediatR;
using FinTasker.Application.Common.Models;
using FinTasker.Application.Common.Interfaces.Service;
using FinTasker.Application.Features.Tasks.DTOs;
using FinTasker.Application.Common.Interfaces.Repository;
using FinTasker.Application.Common.Exceptions;
using FinTasker.Domain.Enums;

namespace FinTasker.Application.Features.Tasks.Commands.CreateTasks
{
    public class CreateTasksHandler
        : IRequestHandler<CreateTasksCommand, ApiResponse<TaskDto>>

    {
        private readonly ITasksRepository _tasksRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IProjectsService _projectsService;
        private readonly ITaskActivityService _activityService;

        public CreateTasksHandler(ITasksRepository tasksRepository, IProjectsService projectsService, ICurrentUserService currentUserService, ITaskActivityService activityService)
        {
            _currentUserService = currentUserService;
            _tasksRepository = tasksRepository;
            _projectsService = projectsService;
            _activityService = activityService;
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
                Id = Guid.NewGuid(),    
                ProjectId = request.ProjectId,
                Title = request.Title,
                Description = request.Description,
                Priority = request.Priority,
                Status = request.Status,
                DueDate = request.DueDate,
                Estimed_Minutes = request.Estimed_Minutes,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow,
            };

            // Save
            await _tasksRepository.CreateTasksAsync(newTasks, cancellationToken);

            // Log Activity setelah add task
            await _activityService.LogAsync(
                Id:       newTasks.Id,
                activityType: ActivityType.Created,
                description:  $"Task \"{newTasks.Title}\" was created.",
                ct:           cancellationToken);

            // response
            var response = new TaskDto
            {
                Id = newTasks.Id,
                ProjectId = newTasks.ProjectId,
                Title = newTasks.Title,
                Description = newTasks.Description,
                Priority = newTasks.Priority.ToString(),
                Status = newTasks.Status.ToString(),
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



