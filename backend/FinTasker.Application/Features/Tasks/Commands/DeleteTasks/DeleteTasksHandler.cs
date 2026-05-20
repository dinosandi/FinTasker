using MediatR;
using FinTasker.Application.Common.Models;
using FinTasker.Application.Common.Interfaces.Repository;
using FinTasker.Application.Common.Interfaces.Service;
using FinTasker.Application.Common.Exceptions;


namespace FinTasker.Application.Features.Tasks.Commands.DeleteTasks
{
    public class DeleteTasksHandler : IRequestHandler<DeleteTasksCommand, ApiResponse<string>>
    {
        private readonly ITasksRepository _tasksRepository;
        private readonly ICurrentUserService _currentUserService;
        public DeleteTasksHandler(
            ITasksRepository tasksRepository, // Dependency injection untuk database
            ICurrentUserService currentUserService)
        {
            _tasksRepository = tasksRepository;
            _currentUserService = currentUserService;
        }
        public async Task<ApiResponse<string>> Handle(DeleteTasksCommand request, CancellationToken cancellationToken)
        {
            // cek apakah user login
            var userId = _currentUserService.UserId
                ?? throw new UnauthorizedAccessException("User is not logged in.");

            // cek untuk data task apakah ada
            var task = await _tasksRepository.GetTaskByIdAsync(request.Id);

            if (task == null)
                throw new NotFoundException("Task not found.");

            // cek apakah user memiliki akses untuk menghapus task tersebut
            if (task.Project.UsersId != userId)
                throw new UnauthorizedAccessException("User does not have permission.");

            await _tasksRepository.DeleteTaskAsync(task);

            return new ApiResponse<string>
            {
                Success = true,
                Message = "Task deleted successfully.",
                
            };
        }
    }
}

