using MediatR;
using FinTasker.Application.Common.Exceptions;
using FinTasker.Application.Common.Interfaces.Repository;
using FinTasker.Application.Common.Interfaces.Service;
using FinTasker.Application.Common.Models;
using FinTasker.Application.Features.Projects.DTOs;

namespace FinTasker.Application.Features.Projects.Commands.UpdateProject;

public class UpdateProjectHandler : IRequestHandler<UpdateProjectCommand, ApiResponse<ProjectDto>>
{
    private readonly IProjectsRepository _projectRepository;
    private readonly ICurrentUserService _currentUserService;

    public UpdateProjectHandler(
        IProjectsRepository projectRepository, // Dependency injection untuk database
        ICurrentUserService currentUserService)
    {
        _projectRepository = projectRepository;
        _currentUserService = currentUserService;
    }

    public async Task<ApiResponse<ProjectDto>> Handle(
        UpdateProjectCommand request,
        CancellationToken cancellationToken)
    {
        // cek apakah user login
        var userId = _currentUserService.UserId
            ?? throw new UnauthorizedAccessException(
                "User is not logged in.");

        // cek apakah project dengan id tersebut ada di database
        var project = await _projectRepository
            .GetProjectByIdAsync(request.Id);

        if (project == null)
            throw new NotFoundException("Project not found.");

        if (project.UsersId != userId)
            throw new UnauthorizedAccessException(
                "User does not have permission.");

        project.Name = request.Name;
        project.Description = request.Description;
        project.Status = request.Status;
        project.Color = request.Color;
        project.StartDate = request.StartDate;
        project.EndDate = request.EndDate;
        project.UpdatedAt = DateTimeOffset.UtcNow;

        await _projectRepository.SaveChangesAsync();

        var dto = new ProjectDto
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            Status = project.Status,
            Color = project.Color,
            StartDate = project.StartDate,
            EndDate = project.EndDate,

        };

        return new ApiResponse<ProjectDto>
        {
            Success = true,
            Message = "Project updated successfully.",
            Data = dto
        };

        
    }
}