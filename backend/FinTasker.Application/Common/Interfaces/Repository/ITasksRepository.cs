using System;
using FinTasker.Domain.Entities;

namespace FinTasker.Application.Common.Interfaces.Repository
{
    public interface ITasksRepository
    {
        Task CreateTasksAsync(Tasks task);
        // Task<Tasks> GetTaskByIdAsync(Guid taskId);
        // Task<IEnumerable<Tasks>> GetAllTasksAsync();
        // Task UpdateTaskAsync(Tasks task);
        // Task DeleteTaskAsync(Guid taskId);
        Task SaveChangesAsync();
    }

}

