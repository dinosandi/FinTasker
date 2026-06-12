using Microsoft.AspNetCore.Mvc;
using MediatR;
using FinTasker.Application.Common.Models;
using FinTasker.Application.Features.Tasks.Commands.CreateTasks;
using Microsoft.AspNetCore.Authorization;
using FinTasker.Application.Features.Tasks.DTOs;
using FinTasker.Application.Features.Tasks.Commands.DeleteTasks;
using FinTasker.Application.Features.Tasks.Commands.UpdateTasks;


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
        [HttpPatch("{id:guid}")]
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
    }
}
