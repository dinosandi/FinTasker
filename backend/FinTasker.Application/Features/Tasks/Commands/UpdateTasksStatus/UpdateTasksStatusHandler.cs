using FinTasker.Application.Common.Exceptions;
using FinTasker.Application.Common.Interfaces.Repository;
using FinTasker.Application.Common.Interfaces.Service;
using FinTasker.Application.Common.Models;
using FinTasker.Application.Features.Tasks.DTOs;
using MediatR;

namespace FinTasker.Application.Features.Tasks.Commands.UpdateTasksStatus
{

    public class UpdateTasksStatusHandler : IRequestHandler<UpdateTasksStatusCommand, ApiResponse<TaskDto>>
    {
        private readonly ITasksRepository _tasksRepository;
        private readonly ICurrentUserService _currentUserService;

        public UpdateTasksStatusHandler(ITasksRepository tasksRepository, ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
            _tasksRepository = tasksRepository;
        }

        public async Task<ApiResponse<TaskDto>> Handle(
            UpdateTasksStatusCommand request,
            CancellationToken cancellationToken)

        {
            var userId = _currentUserService.UserId
                ?? throw new UnauthorizedAccessException("User is logged in");

            var task = await _tasksRepository.GetTaskByIdAsync(request.Id);

            if (task == null)
                throw new NotFoundException("Task not found.");

            task.Status = request.Status;

            await _tasksRepository.UpdateTaskAsync(task, cancellationToken);

            var dto = new TaskDto
            {
                Id = task.Id,
                Status = task.Status.ToString()
            };

            return new ApiResponse<TaskDto>
            {
                Success = true,
                Message = "Task status updated successfully",
                Data = dto
            };
        }

    }


}
