using Microsoft.AspNetCore.Mvc;
using MediatR;
using FinTasker.Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using FinTasker.Application.Features.Projects.Queries.GetAllProjects;
using FinTasker.Application.Features.Projects.Queries.GetByIdProject;
using FinTasker.Application.Features.Projects.DTOs;

namespace FinTasker.API.Controller.Project.Queries
{
    [Route("api/project")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<ProjectDto>>>> GetAllProjects(
            [FromQuery] GetAllProjectQuery query,  // ← binding dari query string
            CancellationToken ct)
        {
            var result = await _mediator.Send(query, ct);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetByIdProjectQuery(id), ct);
            return Ok(result);
        }
        

    }
}
