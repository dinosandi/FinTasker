using FinTasker.Application.Common.Interfaces.Service;
using FinTasker.Domain.Entities;
using FinTasker.Application.Common.Interfaces.Repository;


namespace FinTasker.Infrastructure.Services
{
    public class ProjectsService : IProjectsService
    {
        private readonly IProjectsRepository _repository; // Menggunakan repository untuk operasi database

        public ProjectsService(IProjectsRepository repository) // Dependency Injection untuk mendapatkan instance repository
        {
            _repository = repository;
        }

        public async Task CreateProjectsAsync(Projects project)
        {
            await _repository.CreateProjectsAsync(project);
        }

        // public async Task UpdateProjectAsync(Projects project)
        // {
        //     await _repository.UpdateProjectAsync(project);
        // }

        // public async Task DeleteProjectAsync(Projects project)
        // {
        //     await _repository.DeleteProjectAsync(project);
        // }

        public async Task<Projects> GetProjectsByIdAsync(Guid projectId)
        {
            return await _repository.GetProjectByIdAsync(projectId);
        }

    }
}

