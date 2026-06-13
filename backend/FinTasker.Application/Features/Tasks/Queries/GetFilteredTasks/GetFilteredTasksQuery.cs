using FinTasker.Application.Common.Models;
using FinTasker.Application.Features.Tasks.DTOs;
using FinTasker.Domain.Enums;
using MediatR;

namespace FinTasker.Application.Features.Tasks.Queries.GetFilteredTasks
{
    public class GetFilteredTasksQuery
        : IRequest<ApiResponse<List<TaskFilteredDto>>>
    {
        public Guid? ProjectId { get; set; }

        public StatusTask? Status { get; set; }

        public TaskPriority? Priority { get; set; }

        public string? Tag { get; set; }

        public string? Search { get; set; }

        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}