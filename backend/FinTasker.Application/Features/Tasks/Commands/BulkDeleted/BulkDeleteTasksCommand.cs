using MediatR;
using FinTasker.Application.Common.Models;

namespace FinTasker.Application.Features.Tasks.Commands.BulkDeleted
{
    public class BulkDeleteTasksCommand : IRequest<ApiResponse<string>>
    {
        public List<Guid> TaskIds { get; set; } = new();
    }
}