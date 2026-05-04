using Microsoft.AspNetCore.Mvc;
using MediatR;
using FinTasker.Application.Features.Auth.Commands.Login;
using FinTasker.Application.Common.Models;
using Microsoft.AspNetCore.Authentication;

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


        /// Login menggunakan Google

        [HttpPost("google-login")]
        public async Task<ActionResult<ApiResponse<AuthResponse>>> LoginWithGoogle([FromBody] LoginCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
        
         [HttpGet("google-response")]
    public async Task<IActionResult> GoogleResponse()
    {
        var result = await HttpContext.AuthenticateAsync();

        if (!result.Succeeded)
            return BadRequest("Google authentication failed");

        var claims = result.Principal.Identities
            .FirstOrDefault()?.Claims;

        return Ok(claims.Select(c => new { c.Type, c.Value }));
    }
    }
}