using FinTasker.Application.Common.Models;
using FinTasker.Application.Features.Tasks.Commands.BulkDeleted;
using FinTasker.Application.Features.Tasks.Commands.CreateTasks;
using FinTasker.Application.Features.Tasks.Commands.DeleteTasks;
using FinTasker.Application.Features.Tasks.Commands.UpdateTasks;
using FinTasker.Application.Features.Tasks.Commands.UpdateTasksStatus;
using FinTasker.Application.Features.Tasks.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FinTasker.Application.Features.Tasks.Queries;
using FinTasker.Application.Features.Tasks.Queries.GetFilteredTasks;
using FinTasker.Application.Features.Tasks.Commands.UpdateTasksPriority;


namespace FinTasker.API.Controller.Tasks
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TasksController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ApiResponse<TaskDto>>> CreateTask(
[FromBody] CreateTasksCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
        [Authorize]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteTask(
    [FromRoute] Guid id)
        {
            var command = new DeleteTasksCommand(id);

            var result = await _mediator.Send(command);

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [Authorize]
        [HttpDelete("bulk")]
        public async Task<IActionResult> BulkDelete([FromBody] BulkDeleteTasksCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ApiResponse<TaskDto>>> UpdateTask(
            [FromRoute] Guid id,
            [FromBody] UpdateTasksCommand command)
        {
            if (id != command.Id)
                return BadRequest(new ApiResponse<TaskDto>
                {
                    Success = false,
                    Message = "ID in the route does not match ID in the body."
                });

            var result = await _mediator.Send(command);

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [Authorize]
        [HttpPatch("{id:guid}/status")]
        public async Task<ActionResult<ApiResponse<TaskDto>>> UpdateTask(
            [FromRoute] Guid id,
            [FromBody] UpdateTasksStatusCommand command
        )
        {
            if (id != command.Id)
                return BadRequest(new ApiResponse<TaskDto>
                {
                    Success = false,
                    Message = "ID in the route does not match ID in the body"
                });
            var result = await _mediator.Send(command);

            return result.Success ? Ok(result) : BadRequest(result);
        }
        [Authorize]
        [HttpPatch("{id:guid}/priority")]
        public async Task<ActionResult<ApiResponse<TaskDto>>> UpdateTask(
            [FromRoute] Guid id,
            [FromBody] UpdateTasksPriorityCommand command
        )
        {
            if (id != command.Id)
                return BadRequest(new ApiResponse<TaskDto>
                {
                    Success = false,
                    Message = "ID in the route does not match ID in the body"
                });
            var result = await _mediator.Send(command);

            return result.Success ? Ok(result) : BadRequest(result);
        }
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<PaginatedResult<TaskFilteredDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse<List<TaskFilteredDto>>>> GetFilteredTasks(
        [FromQuery] GetFilteredTasksQuery query,
        CancellationToken ct)
        {
            var result = await _mediator.Send(query, ct);
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
