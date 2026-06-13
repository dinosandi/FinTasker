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
        
        public IQueryable<Tasks> GetQueryable()
        => _context.Tasks;

        public async Task CreateTasksAsync(Tasks tasks, CancellationToken ct = default)
        {
            await _context.Tasks.AddAsync(tasks, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task<Tasks> GetTaskByIdAsync(Guid Id, CancellationToken ct = default)
        => await _context.Tasks
        .AsNoTracking()
        .FirstOrDefaultAsync(t => t.Id == Id, ct);
        
        public async Task DeleteTaskAsync(Tasks task, CancellationToken ct = default)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync(ct);
        }
        

        public async Task UpdateTaskAsync(Tasks task , CancellationToken ct = default)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync(ct);
        }

        // public async Task DeleteTaskAsync(Tasks task)
        // {
        //     _context.Tasks.Remove(task);
        //     await _context.SaveChangesAsync();
        // }

    }
}