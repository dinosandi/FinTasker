using FinTasker.Domain.Enums;

namespace FinTasker.Application.Features.Projects.Commands.UpdateProject
{
    public record UpdateProjectRequest(
    string Name,
    string Description,
    StatusProjects Status,
    string Color,
    DateTimeOffset? StartDate,
    DateTimeOffset? EndDate
);

}

