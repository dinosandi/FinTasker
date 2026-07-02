using MediatR;
using FinTasker.Application.Common.Models;
using FinTasker.Application.Features.Tasks.DTOs;
using FinTasker.Application.Common.Interfaces.Repository;
using FinTasker.Application.Common.Interfaces.Service;
using FinTasker.Application.Common.Exceptions;
using FinTasker.Domain.Enums;

namespace FinTasker.Application.Features.Tasks.Commands.UpdateTasksPriority
{
    public class UpdateTasksPriorityHandler : IRequestHandler<UpdateTasksPriorityCommand, ApiResponse<TaskDto>>
    {
        private readonly ITasksRepository _tasksRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly ITaskActivityService _activityService;
        private readonly INotificationService _notificationService;

        public UpdateTasksPriorityHandler(ITasksRepository tasksRepository, ICurrentUserService currentUserService, ITaskActivityService activityService, INotificationService notificationService)
        {
            _tasksRepository = tasksRepository;
            _currentUserService = currentUserService;
            _activityService = activityService;
            _notificationService = notificationService;
        }

        public async Task<ApiResponse<TaskDto>> Handle(UpdateTasksPriorityCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId
                ?? throw new UnauthorizedAccessException("User is not logged in.");

            var task = await _tasksRepository.GetTaskByIdAsync(request.Id, cancellationToken)
                  ?? throw new NotFoundException("Task not found.");

            if (task.Project.UsersId != userId)
                throw new UnauthorizedException("User does not have permission.");

            var previousPriority = task.Priority;

            if (previousPriority == request.Priority)
                return ApiResponse<TaskDto>.Ok(MapToDto(task), "Priority is already up to date.");

            task.Priority = request.Priority;
            task.UpdatedAt = DateTimeOffset.UtcNow;

            await _tasksRepository.UpdateTaskAsync(task, cancellationToken);

            // activity log
            var activityDescription = request.Priority == TaskPriority.Critical
                ? $"Task priority changed from {previousPriority} to {request.Priority}. This task is now critical!"
                : $"Task priority changed from {previousPriority} to {request.Priority}.";

            await _activityService.LogAsync(
                Id: task.Id,
                activityType: request.Priority == TaskPriority.Critical ? ActivityType.Updated : ActivityType.PriorityChanged,
                description: activityDescription,
                ct: cancellationToken
            );

            var notifTitle = request.Priority == TaskPriority.Critical
                ? "Task Priority Changed to Critical"
                : "Task Priority Updated";

            var notifMessage = request.Priority == TaskPriority.Critical
                ? $"Task priority changed from {previousPriority} to {request.Priority}. This task is now critical!"
                : $"Task priority changed from {previousPriority} to {request.Priority}.";

            await _notificationService.SendToUserAsync(
                userId: userId,
                title: notifTitle,
                message: notifMessage,
                type: request.Priority == TaskPriority.Critical ? NotificationType.General : NotificationType.TaskPriorityChanged,
                ct: cancellationToken
            );

            return ApiResponse<TaskDto>.Ok(MapToDto(task), "Task priority updated.");
        }
        private static TaskDto MapToDto(Domain.Entities.Tasks task) => new()
        {
            Id = task.Id,
            ProjectId = task.ProjectId,
            Title = task.Title,
            Description = task.Description,
            Status = task.Status.ToString(),
            Priority = task.Priority.ToString(),
            DueDate = task.DueDate,
            CompletedAt = task.CompletedAt,
            Estimed_Minutes = task.Estimed_Minutes,
        };


    }


}

