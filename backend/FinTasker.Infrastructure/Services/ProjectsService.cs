using FinTasker.Application.Common.Interfaces.Service;
using FinTasker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using FinTasker.Infrastructure.Persistence;


namespace FinTasker.Infrastructure.Services
{
    public class ProjectsService : IProjectsService
    {
        private readonly AppDbContext _context;

        public ProjectsService(AppDbContext context) // Dependency Injection untuk mendapatkan instance AppDbContext
        {
            _context = context;
        }

        public async Task CreateProjectsAsync(Projects project)
        {
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
        }
        public async Task<Projects> GetProjectsByIdAsync(Guid projectId)
        {
            return await _context.Projects
            .AsNoTracking() // Menambahkan AsNoTracking untuk meningkatkan performa saat hanya membaca data
            .FirstOrDefaultAsync(p => p.Id == projectId);
        }
        

        // public async Task UpdateProjectAsync(Projects project)
        // {
        //     _context.Projects.Update(project);
        //     await _context.SaveChangesAsync();
        // }

        // public async Task DeleteProjectAsync(Projects project)
        // {
        //     _context.Projects.Remove(project);
        //     await _context.SaveChangesAsync();
        // }
        //     public async Task SaveChangesAsync()
        //     {
        //         await _context.SaveChangesAsync();
        //     }
    }
}

