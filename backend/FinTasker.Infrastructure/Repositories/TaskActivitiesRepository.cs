using FinTasker.Application.Common.Interfaces;
using FinTasker.Application.Common.Interfaces.Repository;
using FinTasker.Domain.Entities;

namespace FinTasker.Infrastructure.Repositories
{
    public class TaskActivitiesRepository : ITaskActivitiesRepository
    {
        private readonly IAppDbContext _context;

        public TaskActivitiesRepository(IAppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TaskActivities activity, CancellationToken ct = default)
        {
            await _context.TaskActivities.AddAsync(activity, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task AddRangeAsync(IEnumerable<TaskActivities> activities, CancellationToken ct = default)
        {
            await _context.TaskActivities.AddRangeAsync(activities, ct);
            await _context.SaveChangesAsync(ct);
        }

        public IQueryable<TaskActivities> GetQueryable()
            => _context.TaskActivities.AsQueryable();
    }
}

