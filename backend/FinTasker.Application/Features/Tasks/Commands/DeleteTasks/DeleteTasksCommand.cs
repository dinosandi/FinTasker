using MediatR;

using FinTasker.Application.Common.Models;

namespace FinTasker.Application.Features.Tasks.Commands.DeleteTasks
{
    public record DeleteTasksCommand(
        Guid Id
    ) : IRequest<ApiResponse<string>>;
}

