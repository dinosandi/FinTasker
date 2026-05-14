using FinTasker.Application.Common.Interfaces.Repository;
using FinTasker.Domain.Entities;
using FinTasker.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FinTasker.Infrastructure.Repositories
{
    public class TasksRepository : ITasksRepository
    {
        private readonly AppDbContext _context;
        public TasksRepository(AppDbContext context) // Dependency Injection untuk mendapatkan instance AppDbContext
        {
            _context = context;
        }

        public async Task CreateTasksAsync(Tasks task)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
        }

        // public async Task UpdateTaskAsync(Tasks task)
        // {
        //     _context.Tasks.Update(task);
        //     await _context.SaveChangesAsync();
        // }

        // public async Task DeleteTaskAsync(Tasks task)
        // {
        //     _context.Tasks.Remove(task);
        //     await _context.SaveChangesAsync();
        // }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}