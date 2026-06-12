using MediatR;
using FinTasker.Application.Common.Models;
using FinTasker.Application.Features.Tasks.DTOs;

namespace FinTasker.Application.Features.Tasks.Queries.GetAllTasks
{

    public record GetByIdTasksQuery(Guid Id) : IRequest<ApiResponse<TaskDto>>;


}
