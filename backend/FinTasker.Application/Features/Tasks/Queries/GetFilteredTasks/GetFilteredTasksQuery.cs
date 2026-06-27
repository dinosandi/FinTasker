using MediatR;
using FinTasker.Application.Common.Models;
using FinTasker.Application.Features.Tasks.DTOs;
using FinTasker.Domain.Enums;

namespace FinTasker.Application.Features.Tasks.Queries.GetFilteredTasks
{
    public record GetFilteredTasksQuery : PaginationQuery,
        IRequest<ApiResponse<List<TaskFilteredDto>>>
    {
        public Guid? ProjectId { get; init; }
        public StatusTask? Status { get; init; }
        public TaskPriority? Priority { get; init; }
        public string? Tag { get; init; }
    }
}