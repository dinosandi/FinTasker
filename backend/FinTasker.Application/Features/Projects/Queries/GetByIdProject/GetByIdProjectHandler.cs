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
                .Include(p => p.Tasks)             
                .FirstOrDefaultAsync(
                    p => p.Id == request.Id,
                    cancellationToken)
                    ?? throw new NotFoundException("Project not found.");

            if (project.UsersId != userId)
                throw new UnauthorizedAccessException("User does not have permission.");

            return new ApiResponse<ProjectDto>
            {
                Success = true,
                Message = "Project by ID successfully fetched.",
                Data = new ProjectDto
                {
                    Name = project.Name,
                    Description = project.Description,
                    Status = project.Status,
                    Color = project.Color,
                    StartDate = project.StartDate,
                    EndDate = project.EndDate,
                    CreatedAt = project.CreatedAt,
                    UpdatedAt = project.UpdatedAt,
                    Tasks = project.Tasks.Select(t => new TaskDto
                    {
                        Title = t.Title,
                        Description = t.Description,
                        Status = t.Status,
                        Priority = t.Priority,
                        DueDate = t.DueDate,
                        CompletedAt = t.CompletedAt,
                        Estimed_Minutes = t.Estimed_Minutes,
                    }).ToList()
                }
            };
            
        }
    }
}
