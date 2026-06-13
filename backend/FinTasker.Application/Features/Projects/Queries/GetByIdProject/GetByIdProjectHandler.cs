using MediatR;
using FinTasker.Application.Common.Models;
using FinTasker.Application.Common.Interfaces.Repository;
using FinTasker.Application.Common.Interfaces.Service;
using Microsoft.EntityFrameworkCore;
using FinTasker.Application.Features.Projects.DTOs;
using FinTasker.Application.Features.Tasks.DTOs;
using FinTasker.Application.Common.Exceptions;

namespace FinTasker.Application.Features.Projects.Queries.GetByIdProject
{
    public class GetByIdProjectHandler : IRequestHandler<GetByIdProjectQuery, ApiResponse<ProjectDto>>
    {
        private readonly IProjectsRepository _projectsRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetByIdProjectHandler(
            IProjectsRepository projectsRepository,
            ICurrentUserService currentUserService)
        {
            _projectsRepository = projectsRepository;
            _currentUserService = currentUserService;
        }

        public async Task<ApiResponse<ProjectDto>> Handle(GetByIdProjectQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId
                ?? throw new UnauthorizedAccessException("User is not logged in.");

            var project = await _projectsRepository
                    .GetQueryable()
                    .AsNoTracking()
                    .Where(p =>
                        p.Id == request.Id &&
                        p.UsersId == userId)
                    .Select(p => new ProjectDto
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        Status = p.Status,
                        Color = p.Color,
                        StartDate = p.StartDate,
                        EndDate = p.EndDate,
                        CreatedAt = p.CreatedAt,
                        UpdatedAt = p.UpdatedAt,

                        Tasks = p.Tasks.Select(t => new TaskDto
                        {
                            Id = t.Id,
                            Title = t.Title,
                            Description = t.Description,
                            Status = t.Status,
                            Priority = t.Priority,
                            DueDate = t.DueDate,
                            CompletedAt = t.CompletedAt,
                            Estimed_Minutes = t.Estimed_Minutes
                        }).ToList()
                    })
                    .FirstOrDefaultAsync(cancellationToken);
            if (project is null)
                throw new NotFoundException("Project not found.");

            return ApiResponse<ProjectDto>.SuccessResponse(
                project,
                "Project retrieved successfully."
            );

        }
    }
}
