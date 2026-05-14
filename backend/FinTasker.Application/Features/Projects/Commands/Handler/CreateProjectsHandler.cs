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

        public CreateProjectsHandler(
            IProjectsService projectsService,
            ICurrentUserService currentUserService)
        {
            _projectsService = projectsService;
            _currentUserService = currentUserService;
        }

        public async Task<ApiResponse<CreateProjectsResponse>> Handle(
            CreateProjectsCommand request,
            CancellationToken cancellationToken)
        {
            // Validasi login
            var userId = _currentUserService.UserId
                ?? throw new UnauthorizedAccessException(
                    "User is not logged in."
                );

            // Mapping entity
            var newProjects = new Domain.Entities.Projects
            {

                UsersId = userId,
                Name = request.Name,
                Description = request.Description,
                Status = request.Status,
                Color = request.Color,
                StartDate = request.StartDate,
                EndDate = request.EndDate,

                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            };

            // Save
            await _projectsService.CreateProjectsAsync(newProjects);

            // Response
            var response = new CreateProjectsResponse
            {
                Name = newProjects.Name,
                Description = newProjects.Description,
                Status = newProjects.Status,
                StartDate = newProjects.StartDate,
                EndDate = newProjects.EndDate
            };

            return ApiResponse<CreateProjectsResponse>.SuccessResponse(
                response,
                "Project created successfully"
            );
        }
    }
}