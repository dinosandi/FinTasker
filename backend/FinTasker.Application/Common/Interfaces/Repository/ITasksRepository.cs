using System;
using FinTasker.Domain.Entities;

namespace FinTasker.Application.Common.Interfaces.Repository
{
    public interface ITasksRepository
    {
        // untuk query flexible (pagination, filter, sort) — tidak perlu GetAllTasksAsync
        // IQueryable<Tasks> GetQueryable();
        Task CreateTasksAsync(Tasks tasks, CancellationToken ct = default);
        Task<Tasks> GetTaskByIdAsync(Guid Id, CancellationToken ct = default);
        // Task<IEnumerable<Tasks>> GetAllTasksAsync();
        // Task UpdateTaskAsync(Tasks task);
        Task DeleteTaskAsync(Tasks task, CancellationToken ct = default);
        
        Task UpdateTaskAsync(Tasks task, CancellationToken ct = default);
    }

}

