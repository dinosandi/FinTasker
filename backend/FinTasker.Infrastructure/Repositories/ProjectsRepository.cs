using FinTasker.Domain.Entities;
using FinTasker.Application.Common.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using FinTasker.Infrastructure.Persistence;

namespace FinTasker.Infrastructure.Repositories // Implementasi repository untuk entitas Projects, menggunakan AppDbContext untuk operasi database
{
    public class ProjectsRepository : IProjectsRepository
    {
        private readonly AppDbContext _context;
        public ProjectsRepository(AppDbContext context) // Dependency Injection untuk mendapatkan instance AppDbContext
        {
            _context = context;
        }

        public async Task<Projects> GetProjectByIdAsync(Guid projectId)
        {
            return await _context.Projects
                .FirstOrDefaultAsync(p => p.Id == projectId);
        }

        public async Task CreateProjectsAsync(Projects project)
        {
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProjectAsync(Projects project)
        {
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProjectAsync(Projects project)
        {
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

