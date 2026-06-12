using MediatR;
using FinTasker.Application.Common.Models;
using FinTasker.Application.Features.Tasks.DTOs;
using FinTasker.Domain.Enums;

namespace FinTasker.Application.Features.Tasks.Commands.UpdateTasks
{
    public record UpdateTasksCommand(
        Guid Id,
        string Title,
        string Description,
        StatusTask Status,
        TaskPriority Priority,
        DateTimeOffset DueDate,
        int Estimed_Minutes,
        DateTimeOffset StartDate,
        DateTimeOffset EndDate
    ) : IRequest<ApiResponse<TaskDto>>;
}
