using Microsoft.AspNetCore.Mvc;
using MediatR;
using FinTasker.Application.Common.Models;
using FinTasker.Application.Features.Tasks.Commands.CreateTasks;
using Microsoft.AspNetCore.Authorization;
using FinTasker.Application.Features.Tasks.DTOs;
using FinTasker.Application.Features.Tasks.Commands.DeleteTasks;
using FinTasker.Application.Features.Tasks.Commands.UpdateTasks;
using FinTasker.Domain.Enums;
using FinTasker.Application.Features.Tasks.Queries.GetFilteredTasks;
using FinTasker.Application.Features.Tasks.Commands.UpdateTasksStatus;
using FinTasker.Application.Features.Tasks.Commands.BulkDeleted;


namespace FinTasker.API.Controller
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


        [HttpPost]
        [Authorize]
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
        
    }
}
