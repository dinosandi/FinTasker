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

namespace FinTasker.API.Controller.Tasks.Command
{
    [Route("api/tasks")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly IMediator _mediator;

        private TasksController(IMediator mediator)
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

    }
}
