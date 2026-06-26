using MediatR;
using Microsoft.EntityFrameworkCore;
using FinTasker.Application.Common.Interfaces;
using FinTasker.Application.Common.Interfaces.Repository;
using FinTasker.Application.Features.Tasks.DTOs;
using FinTasker.Domain.Enums;
using FinTasker.Application.Common.Interfaces.Service;

namespace FinTasker.Application.Features.Tasks.Queries.GetTodayTasks
{

    public sealed class GetTasksDistributionHandler
        : IRequestHandler<GetTasksDistributionQuery, TasksDistributionDto>
    {
        private readonly ITasksRepository _tasksRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetTasksDistributionHandler(
            ITasksRepository tasksRepository,
            ICurrentUserService currentUserService)
        {
            _tasksRepository = tasksRepository;
            _currentUserService = currentUserService;
        }

        public async Task<TasksDistributionDto> Handle(
            GetTasksDistributionQuery request,
            CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId
                ?? throw new UnauthorizedAccessException("User is not logged in.");

            
            var counts = await _tasksRepository
                .GetQueryable()
                .Where(t => t.Project.UsersId == userId)
                .GroupBy(t => t.Status)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToListAsync(cancellationToken);

            var map = counts.ToDictionary(x => x.Status, x => x.Count);

            int Get(StatusTask s) => map.GetValueOrDefault(s, 0);

            return new TasksDistributionDto
            {
                Total = map.Values.Sum(),
                Todo = Get(StatusTask.ToDo),
                InProgress = Get(StatusTask.InProgress),
                Review = Get(StatusTask.Review),
                Completed = Get(StatusTask.Completed),
                Cancelled = Get(StatusTask.Cancelled),
            };
        }
    }

}
