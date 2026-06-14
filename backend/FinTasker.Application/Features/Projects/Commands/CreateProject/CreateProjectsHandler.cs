using MediatR;
using FinTasker.Application.Common.Models;
using FinTasker.Application.Common.Interfaces.Repository;
using FinTasker.Application.Common.Interfaces.Service;

namespace FinTasker.Application.Features.Projects.Commands.CreateProject
{
    public class CreateProjectsHandler
        : IRequestHandler<CreateProjectsCommand, ApiResponse<CreateProjectsResponse>>
    {
        private readonly IProjectsRepository _projectRepository;
        private readonly ICurrentUserService _currentUserService;

        public CreateProjectsHandler(
            IProjectsRepository projectsRepository,
            ICurrentUserService currentUserService)
        {
            _projectRepository = projectsRepository;
            _currentUserService = currentUserService;
        }

        public async Task<ApiResponse<CreateProjectsResponse>> Handle(
            CreateProjectsCommand request,
            CancellationToken ct) // ct di parameter ini digunakan untuk membatalkan operasi jika diperlukan, misalnya jika permintaan dibatalkan oleh klien atau jika operasi memakan waktu terlalu lama.
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
            await _projectRepository.CreateProjectAsync(newProjects, ct);
            

            // Response
            var response = new CreateProjectsResponse
            {
                Name = newProjects.Name,
                Description = newProjects.Description,
                Status = newProjects.Status.ToString(),
                StartDate = newProjects.StartDate,
                EndDate = newProjects.EndDate
            };

            return ApiResponse<CreateProjectsResponse>.Created(
                response,
                "Project created successfully"
            );
        }
    }
}