using MediatR;
using FinTasker.Application.Common.Models;
using FinTasker.Application.Features.Projects.DTOs;

namespace FinTasker.Application.Features.Projects.Queries.GetAllProjects
{

    public record GetAllProjectQuery : PaginationQuery,   // ← inherit pagination properties
        IRequest<ApiResponse<PaginatedResult<ProjectDto>>>;
}
