using MediatR;
using FinTasker.Application.Common.Models;
using FinTasker.Application.Features.Projects.DTOs;
using FinTasker.Domain.Enums;

namespace FinTasker.Application.Features.Projects.Queries.GetAllProjects
{
    public record GetAllProjectQuery : PaginationQuery,   // ← inherit pagination properties
        IRequest<ApiResponse<List<ProjectDto>>>
    {
            public StatusProjects? Status { get; init; }
        }
        

}
