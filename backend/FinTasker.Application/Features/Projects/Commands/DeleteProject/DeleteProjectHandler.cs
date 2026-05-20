using MediatR;
using FinTasker.Application.Common.Exceptions;
using FinTasker.Application.Common.Interfaces.Repository;
using FinTasker.Application.Common.Interfaces.Service;
using FinTasker.Application.Common.Models;


namespace FinTasker.Application.Features.Projects.Commands.DeleteProject
{
    public class DeleteProjectHandler : IRequestHandler<DeleteProjectCommand, ApiResponse<string>>
    {
        private readonly IProjectsRepository _projectRepository;
        private readonly ICurrentUserService _currentUserService;

        public DeleteProjectHandler(
            IProjectsRepository projectRepository, // Dependency injection untuk database
            ICurrentUserService currentUserService)
        {
            _projectRepository = projectRepository;
            _currentUserService = currentUserService;
        }

        public async Task<ApiResponse<string>> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            // cek apakah user login
            var userId = _currentUserService.UserId
                ?? throw new UnauthorizedAccessException("User is not logged in.");

            // cek untuk data project apakah ada
            var project = await _projectRepository.GetProjectByIdAsync(request.Id);

            if (project == null)
                throw new NotFoundException("Project not found.");

            // cek apakah user memiliki akses untuk menghapus project tersebut
            if (project.UsersId != userId)
                throw new UnauthorizedAccessException("User does not have permission.");

            await _projectRepository.DeleteProjectAsync(project);

            return new ApiResponse<string>
            {
                Success = true,
                Message = "Project deleted successfully.",
                
            };

        } 
    }

}

