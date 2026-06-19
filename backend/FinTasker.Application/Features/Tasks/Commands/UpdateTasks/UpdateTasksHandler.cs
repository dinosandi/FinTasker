using MediatR;
using FinTasker.Application.Common.Models;
using FinTasker.Application.Features.Tasks.DTOs;
using FinTasker.Application.Common.Exceptions;
using FinTasker.Application.Common.Interfaces.Repository;
using FinTasker.Application.Common.Interfaces.Service;

namespace FinTasker.Application.Features.Tasks.Commands.UpdateTasks
{
    public class UpdateTasksHandler : IRequestHandler<UpdateTasksCommand, ApiResponse<TaskDto>>
    {
        private readonly ITasksRepository _tasksRepository;
        private readonly ICurrentUserService _currentUserService;

        public UpdateTasksHandler(ITasksRepository tasksRepository, ICurrentUserService currentUserService)
        {
            _tasksRepository = tasksRepository;
            _currentUserService = currentUserService;
        }

        public async Task<ApiResponse<TaskDto>> Handle(
            UpdateTasksCommand request,
            CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId
                ?? throw new UnauthorizedAccessException("User is not logged in.");

            var task = await _tasksRepository
                .GetTaskByIdAsync(request.Id);

            if (task == null)
                throw new NotFoundException("Task not found.");

            task.Title = request.Title;
            task.Description = request.Description;
            task.Status = request.Status;
            task.Priority = request.Priority;
            task.DueDate = request.DueDate;
            task.Estimed_Minutes = request.Estimed_Minutes;
            task.DueDate = request.DueDate;

            await _tasksRepository.UpdateTaskAsync(task, cancellationToken);

            var dto = new TaskDto
            {
                ProjectId = task.ProjectId,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status.ToString(),
                Priority = task.Priority.ToString(),
                DueDate = task.DueDate,
                Estimed_Minutes = task.Estimed_Minutes
            };
            return new ApiResponse<TaskDto>
            {
                Success = true,
                Message = "Task successfully updated.",
                Data = dto
            };
        }
    }

}

