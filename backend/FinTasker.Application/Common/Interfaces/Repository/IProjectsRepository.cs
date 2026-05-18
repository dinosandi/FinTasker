using System;
using FinTasker.Domain.Entities;

namespace FinTasker.Application.Common.Interfaces.Repository
{
    public interface IProjectsRepository
    {

        IQueryable<Projects> GetQueryable();
      
        Task CreateProjectsAsync(Projects project);
        Task<Projects> GetProjectByIdAsync(Guid projectId);
        Task<IEnumerable<Projects>> GetAllProjectsAsync();
        Task UpdateProjectAsync(Projects project);
        Task DeleteProjectAsync(Projects project);
        Task SaveChangesAsync();
    }
}

