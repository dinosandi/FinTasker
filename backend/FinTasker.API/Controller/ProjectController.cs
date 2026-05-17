using Microsoft.AspNetCore.Mvc;
using MediatR;
using FinTasker.Application.Common.Models;
using FinTasker.Application.Features.Projects.Commands.CreateProject;
using Microsoft.AspNetCore.Authorization;
using FinTasker.Application.Features.Projects.Commands.UpdateProject;
using FinTasker.Application.Features.Projects.DTOs;


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

        [Authorize]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ApiResponse<ProjectDto>>> UpdateProject(
            [FromRoute] Guid id,
            [FromBody] UpdateProjectRequest request)   
        {
            var command = new UpdateProjectCommand(
                id,
                request.Name,
                request.Description,
                request.Status,
                request.Color,
                request.StartDate,
                request.EndDate
            );

            var result = await _mediator.Send(command);

            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}