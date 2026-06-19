using System;
using FinTasker.Domain.Entities;

namespace FinTasker.Application.Common.Interfaces.Repository
{

    public interface ITaskActivitiesRepository
    {
        Task AddAsync(TaskActivities activity, CancellationToken ct = default);

        Task AddRangeAsync(IEnumerable<TaskActivities> activities, CancellationToken ct = default);

        IQueryable<TaskActivities> GetQueryable();

    }


}
