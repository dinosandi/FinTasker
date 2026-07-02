using FinTasker.Application.Common.Exceptions;
using FinTasker.Application.Common.Interfaces.Repository;
using FinTasker.Application.Common.Interfaces.Service;
using FinTasker.Application.Common.Models;
using FinTasker.Application.Features.Tasks.DTOs;
using FinTasker.Domain.Enums;
using MediatR;

namespace FinTasker.Application.Features.Tasks.Commands.UpdateTasksStatus
{

    public class UpdateTasksStatusHandler : IRequestHandler<UpdateTasksStatusCommand, ApiResponse<TaskDto>>
    {
        private readonly ITasksRepository _tasksRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly ITaskActivityService _activityService;
        private readonly INotificationService _notificationService;


        public UpdateTasksStatusHandler(
            ITasksRepository tasksRepository,
            ICurrentUserService currentUserService,
            ITaskActivityService activityService,
            INotificationService notificationService)
        {
            _tasksRepository = tasksRepository;
            _currentUserService = currentUserService;
            _activityService = activityService;
            _notificationService = notificationService;
        }

        public async Task<ApiResponse<TaskDto>> Handle(
                    UpdateTasksStatusCommand request,
                    CancellationToken cancellationToken)
        {
            // ── 1. Guard: user harus login 
            var userId = _currentUserService.UserId
                ?? throw new UnauthorizedException("User is not logged in.");

            // ── 2. Ambil task + project (untuk cek ownership) 
            var task = await _tasksRepository.GetTaskByIdAsync(request.Id, cancellationToken)
                ?? throw new NotFoundException("Task not found.");

            // ── 3. Guard: hanya owner project yang boleh update 
            if (task.Project.UsersId != userId)
                throw new UnauthorizedException("User does not have permission.");

            // ── 4. Simpan status lama untuk activity log 
            var previousStatus = task.Status;

            // tidak perlu update kalau status sama
            if (previousStatus == request.Status)
                return ApiResponse<TaskDto>.Ok(MapToDto(task), "Status is already up to date.");

            // ── 5. Update status 
            task.Status = request.Status;
            task.UpdatedAt = DateTimeOffset.UtcNow;

            // ── 6. Kalau status → Completed, isi CompletedAt 
            if (request.Status == StatusTask.Completed)
                task.CompletedAt = DateTimeOffset.UtcNow;
            else if (previousStatus == StatusTask.Completed)
                task.CompletedAt = default;

            await _tasksRepository.UpdateTaskAsync(task, cancellationToken);

            // ── 7. Activity log 
            var activityDescription = request.Status == StatusTask.Completed
                ? $"Task \"{task.Title}\" marked as Completed."
                : $"Task \"{task.Title}\" status changed from {previousStatus} to {request.Status}.";

            await _activityService.LogAsync(
                Id: task.Id,
                activityType: request.Status == StatusTask.Completed
                                  ? ActivityType.Completed
                                  : ActivityType.StatusChanged,
                description: activityDescription,
                ct: cancellationToken);

            // ── 8. Trigger notification 
            var notifTitle = request.Status == StatusTask.Completed
                ? "Task Completed"
                : "Task Status Updated";

            var notifMessage = request.Status == StatusTask.Completed
                ? $"Task \"{task.Title}\" has been marked as completed."
                : $"Task \"{task.Title}\" status changed to {request.Status}.";

            await _notificationService.SendToUserAsync(
                userId: userId,
                title: notifTitle,
                message: notifMessage,
                type: request.Status == StatusTask.Completed
                             ? NotificationType.TaskCompleted
                             : NotificationType.TaskStatusChanged,
                ct: cancellationToken);

            // ── 9. Return response 
            return ApiResponse<TaskDto>.Ok(
                MapToDto(task),
                "Task status updated successfully");
        }

        // ── Private helper 
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
