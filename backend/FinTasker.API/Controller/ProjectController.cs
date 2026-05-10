using Microsoft.AspNetCore.Mvc;
using MediatR;
using FinTasker.Application.Common.Models;
using FinTasker.Application.Features.Projects.Commands.Command;
using Microsoft.AspNetCore.Authorization;


namespace FinTasker.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ApiResponse<CreateProjectsResponse>>> CreateProject(
            [FromBody] CreateProjectsCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}