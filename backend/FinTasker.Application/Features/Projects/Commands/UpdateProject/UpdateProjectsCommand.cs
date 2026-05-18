using MediatR;
using FinTasker.Domain.Enums;
using FinTasker.Application.Features.Projects.DTOs;
using FinTasker.Application.Common.Models;

namespace FinTasker.Application.Features.Projects.Commands.UpdateProject
{
    public record UpdateProjectCommand (
        Guid Id,
        string Name,
        string Description,
        StatusProjects Status,
        string Color,
        DateTimeOffset? StartDate,
        DateTimeOffset? EndDate
    ) : IRequest<ApiResponse<ProjectDto>>;
}

