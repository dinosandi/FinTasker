using System;
using FinTasker.Domain.Entities;

namespace FinTasker.Application.Common.Interfaces.Repository
{
    public interface IProjectsRepository
    {
      
        Task CreateProjectsAsync(Projects project);
        Task<Projects> GetProjectByIdAsync(Guid projectId);
        // Task<IEnumerable<Projects>> GetAllProjectsAsync();
        // Task UpdateProjectAsync(Projects project);
        // Task DeleteProjectAsync(Guid projectId);
        Task SaveChangesAsync();
    }
}

