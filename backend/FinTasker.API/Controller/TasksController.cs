using Microsoft.AspNetCore.Mvc;
using MediatR;
using FinTasker.Application.Common.Models;
using FinTasker.Application.Features.Tasks.Commands.Command;
using Microsoft.AspNetCore.Authorization;


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
        public async Task<ActionResult<ApiResponse<CreateTasksResponse>>> CreateTask(
            [FromBody] CreateTasksCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
