using System;
using Microsoft.EntityFrameworkCore;
using FinTasker.Domain.Entities;
using FinTasker.Application.Common.Interfaces.Service;
using FinTasker.Infrastructure.Persistence;


namespace FinTasker.Infrastructure.Services
{
    public class TasksService : ITasksService
    {
        private readonly AppDbContext _context;

        public TasksService(AppDbContext context)
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
    }
}

