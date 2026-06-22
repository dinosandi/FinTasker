using FinTasker.Application.Common.Models;
using FinTasker.Application.Features.Tasks.DTOs;
using FinTasker.Application.Features.Tasks.Queries;
using FinTasker.Application.Features.Tasks.Queries.GetFilteredTasks;
using FinTasker.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinTasker.API.Controller.Tasks.Query
{
    [Route("api/tasks")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TasksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<PaginatedResult<TaskFilteredDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetFilteredTasks(
            [FromQuery] Guid? projectId,
            [FromQuery] StatusTask? status,
            [FromQuery] TaskPriority? priority,
            [FromQuery] string? tag,
            [FromQuery] string? search,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            var query = new GetFilteredTasksQuery
            {
                ProjectId = projectId,
                Status = status,
                Priority = priority,
                Tag = tag,
                Search = search,
                Page = page,
                PageSize = pageSize
            };

            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpGet("distribution")]
        [ProducesResponseType(typeof(ApiResponse<TasksDistributionDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetTasksDistribution(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetTasksDistributionQuery(), cancellationToken);

            return Ok(ApiResponse<TasksDistributionDto>.Ok(
                data: result,
                message: "Successfully retrieved tasks distribution."
            ));
        }

    }
}
