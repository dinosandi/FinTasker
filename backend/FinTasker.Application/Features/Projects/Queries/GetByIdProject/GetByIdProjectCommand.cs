using MediatR;
using FinTasker.Application.Common.Models;
using FinTasker.Application.Features.Projects.DTOs;

namespace FinTasker.Application.Features.Projects.Queries.GetByIdProject
{
    public record GetByIdProjectQuery(Guid Id) : IRequest<ApiResponse<ProjectDto>>;
}
