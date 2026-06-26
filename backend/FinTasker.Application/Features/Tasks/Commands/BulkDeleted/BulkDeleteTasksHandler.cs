using MediatR;
using FinTasker.Application.Common.Models;
using FinTasker.Application.Common.Interfaces.Repository;
using FinTasker.Application.Common.Interfaces.Service;
using FinTasker.Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FinTasker.Application.Features.Tasks.Commands.BulkDeleted
{
    public class BulkDeleteTasksCommandHandler : IRequestHandler<BulkDeleteTasksCommand, ApiResponse<string>>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly ITasksRepository _tasksRepository;

        public BulkDeleteTasksCommandHandler(
            ITasksRepository tasksRepository,
            ICurrentUserService currentUserService)
        {
            _tasksRepository = tasksRepository;
            _currentUserService = currentUserService;
        }

        public async Task<ApiResponse<string>> Handle(
            BulkDeleteTasksCommand request,
            CancellationToken cancellationToken)
        {
            // 1. Auth check
            var userId = _currentUserService.UserId
                ?? throw new UnauthorizedAccessException("User is not logged in.");

            // 2. Fetch semua tasks sekaligus via IQueryable — 1 DB round-trip
            var tasks = await _tasksRepository
                .GetQueryable()
                .Where(t => request.TaskIds.Contains(t.Id))
                .Include(t => t.Project)          // butuh Project.UsersId untuk auth
                .ToListAsync(cancellationToken);

            // 3. Validasi: pastikan semua TaskIds ditemukan
            var foundIds    = tasks.Select(t => t.Id).ToHashSet();
            var missingIds  = request.TaskIds.Except(foundIds).ToList();

            if (missingIds.Count > 0)
                throw new NotFoundException(
                    $"Tasks not found: {string.Join(", ", missingIds)}");

            // 4. Authorization: semua task HARUS milik userId
            var unauthorized = tasks.Any(t => t.Project.UsersId != userId);
            if (unauthorized)
                throw new UnauthorizedAccessException(
                    "User does not have permission to delete one or more tasks.");

            // 5. Bulk delete — single SaveChanges
            await _tasksRepository.BulkDeleteTasksAsync(tasks, cancellationToken);

            return new ApiResponse<string>
            {
                Success = true,
                Message = $"{tasks.Count} task deleted successfully."
            };
        }
    }
}