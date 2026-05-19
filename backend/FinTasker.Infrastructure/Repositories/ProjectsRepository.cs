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

        public IQueryable<Projects> GetQueryable()
            => _context.Projects.AsQueryable();

        public async Task<Projects?> GetProjectByIdAsync(Guid projectId, CancellationToken ct = default)
        => await _context.Projects
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == projectId, ct);

        public async Task CreateProjectAsync(Projects project, CancellationToken ct = default)
        {
            await _context.Projects.AddAsync(project, ct);
            await _context.SaveChangesAsync(ct);  // SaveChanges di dalam repository
        }

        public async Task UpdateProjectAsync(Projects project, CancellationToken ct = default)
        {
            _context.Projects.Update(project);
            await _context.SaveChangesAsync(ct);
        }

        public async Task DeleteProjectAsync(Projects project, CancellationToken ct = default)
        {
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync(ct);
        }


    }
}

