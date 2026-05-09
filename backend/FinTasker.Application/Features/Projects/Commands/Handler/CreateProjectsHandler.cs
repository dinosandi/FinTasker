using MediatR;
using FinTasker.Application.Common.Models;
using FinTasker.Application.Features.Projects.Commands.Command;
using FinTasker.Application.Common.Interfaces.Service;

namespace FinTasker.Application.Features.Projects.Commands.Handler
{
    public class CreateProjectsHandler
        : IRequestHandler<CreateProjectsCommand, ApiResponse<CreateProjectsResponse>>
    {
        private readonly IProjectsService _projectsService;
        private readonly ICurrentUserService _currentUserService;

        public CreateProjectsHandler(IProjectsService projectsService , ICurrentUserService currentUserService)
        {
            _projectsService = projectsService;
            _currentUserService = currentUserService; 
        }

        public async Task<ApiResponse<CreateProjectsResponse>> Handle(
            CreateProjectsCommand request,
            CancellationToken cancellationToken)
        {

            try
            {
                // Mapping dari command ke entity
                var newProjects = new Domain.Entities.Projects
                {
                    Id = Guid.NewGuid(),

                    // nanti ambil dari token/login user
                    UsersId = _currentUserService.GetCurrentUserId(),

                    Name = request.Name,
                    Description = request.Description,
                    Status = request.Status,
                    Color = request.Color,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,

                    CreatedAt = DateTimeOffset.UtcNow,
                    UpdatedAt = DateTimeOffset.UtcNow
                };

                // Simpan data
                await _projectsService.CreateProjectsAsync(newProjects);

                // Mapping response
                var response = new CreateProjectsResponse
                {
                    Name = newProjects.Name,
                    Description = newProjects.Description,
                    Status = newProjects.Status,
                    StartDate = newProjects.StartDate,
                    EndDate = newProjects.EndDate
                };

                return new ApiResponse<CreateProjectsResponse>
                {
                    Data = response,
                    Message = "Project created successfully",
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<CreateProjectsResponse>
                {
                    Success = false,
                    Message = $"Failed to create project: {ex.Message}"
                };
            }

        }
    }
}