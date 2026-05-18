using FinTasker.Application.Common.Models;
using MediatR;

namespace FinTasker.Application.Features.Projects.Commands.DeleteProject
{
    public record DeleteProjectCommand(
        Guid Id
    ) : IRequest<ApiResponse<string>>;
}