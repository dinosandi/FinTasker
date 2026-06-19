using FinTasker.Application.Common.Models;
using FinTasker.Application.Features.Tasks.DTOs;
using FinTasker.Domain.Enums;
using MediatR;

namespace FinTasker.Application.Features.Tasks.Commands.UpdateTasksStatus
{

    public record UpdateTasksStatusCommand(
        Guid Id,
        StatusTask Status
    ) : IRequest<ApiResponse<TaskDto>>;
}
