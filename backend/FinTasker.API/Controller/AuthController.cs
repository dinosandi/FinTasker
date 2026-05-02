using Microsoft.AspNetCore.Mvc;
using MediatR;
using FinTasker.Application.Features.Auth.Commands.Login;
using FinTasker.Application.Common.Models;

namespace FinTasker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Login menggunakan Google
        /// </summary>
        [HttpPost("google-login")]
        public async Task<ActionResult<ApiResponse<AuthResponse>>> LoginWithGoogle([FromBody] LoginCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}