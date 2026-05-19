using FinTasker.Domain.Entities;

namespace FinTasker.Application.Common.Interfaces.Repository;

public interface IProjectsRepository
{
    // Untuk query fleksibel (pagination, filter, sort) — tidak perlu GetAllProjectsAsync
    IQueryable<Projects> GetQueryable();

    Task<Projects?> GetProjectByIdAsync(Guid projectId, CancellationToken ct = default);
    Task CreateProjectAsync(Projects project, CancellationToken ct = default);
    Task UpdateProjectAsync(Projects project, CancellationToken ct = default);
    Task DeleteProjectAsync(Projects project, CancellationToken ct = default);
}