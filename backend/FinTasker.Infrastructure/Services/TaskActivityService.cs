
using FinTasker.Application.Common.Interfaces.Service;
using FinTasker.Application.Common.Interfaces.Repository;
using FinTasker.Domain.Entities;
using FinTasker.Domain.Enums;

namespace FinTasker.Infrastructure.Services
{
    public class TaskActivityService : ITaskActivityService
    {
        private readonly ITaskActivitiesRepository _repo;

        public TaskActivityService(ITaskActivitiesRepository repo)
        {
            _repo = repo;
        }

        public async Task LogAsync(
            Guid taskId,
            ActivityType activityType,
            string description,
            CancellationToken ct = default)
        {
            var activity = new TaskActivities
            {
                Id          = Guid.NewGuid(),
                TasksId     = taskId,
                ActivityType = activityType,
                Description = description,
                CreatedAt   = DateTimeOffset.UtcNow
            };

            await _repo.AddAsync(activity, ct);
        }
    }
}

