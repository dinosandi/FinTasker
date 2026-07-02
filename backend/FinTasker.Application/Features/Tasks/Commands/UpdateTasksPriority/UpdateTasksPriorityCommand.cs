using FinTasker.Application.Common.Models;
using MediatR;
using FinTasker.Domain.Enums;
using FinTasker.Application.Features.Tasks.DTOs;


namespace FinTasker.Application.Features.Tasks.Commands.UpdateTasksPriority
{
    public record UpdateTasksPriorityCommand(
        Guid Id,
        TaskPriority Priority
    ) : IRequest<ApiResponse<TaskDto>>;

}

