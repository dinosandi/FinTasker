using Microsoft.AspNetCore.Mvc;
using MediatR;
using FinTasker.Application.Features.Auth.Commands.LoginWithGoogle;
using FinTasker.Application.Features.Auth.Commands.LoginManualWithEmail;
using FinTasker.Application.Common.Models;
using FinTasker.Application.Features.Auth.Queries.GetCurrentUser;
using Microsoft.AspNetCore.Authorization;
using FinTasker.Application.Features.Auth.Commands.RefreshToken;
using FinTasker.Application.Features.Auth.Commands.Logout;

namespace FinTasker.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
            => _mediator = mediator;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginManualCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);  
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh()
        {
            var result = await _mediator.Send(new RefreshTokenCommand());
            return Ok(result);
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _mediator.Send(new LogoutCommand());
            return NoContent();
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> Me()
        {
            var result = await _mediator.Send(new GetCurrentUserQuery());
            return Ok(result);
        }

        [HttpPost("google-login")]
        public async Task<ActionResult<ApiResponse<AuthResponse>>> LoginWithGoogle([FromBody] LoginWithGoogle command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

    }
}