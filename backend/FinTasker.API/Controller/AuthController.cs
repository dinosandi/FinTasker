using Microsoft.AspNetCore.Mvc;
using MediatR;
using FinTasker.Application.Features.Auth.Commands.LoginWithGoogle;
using FinTasker.Application.Features.Auth.Commands.LoginManualWithEmail;
using FinTasker.Application.Common.Models;
using Microsoft.AspNetCore.Authentication;
using FinTasker.Application.Features.Auth.Commands.Register;
using FinTasker.Application.Common.Interfaces.Service;
using FinTasker.Application.Features.Auth.Queries.GetCurrentUser;
using Microsoft.AspNetCore.Authorization;
using FinTasker.Application.Features.Auth.Commands.RefreshToken;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace FinTasker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICookieService _cookieService;
        private readonly IRefreshTokenService _refreshTokenService;

        public AuthController(IMediator mediator, ICookieService cookieService, IRefreshTokenService refreshTokenService)
        {
            _mediator = mediator;
            _cookieService = cookieService;
            _refreshTokenService = refreshTokenService;
        }


        /// Login menggunakan Google
        [HttpPost("google-login")]
        public async Task<ActionResult<ApiResponse<AuthResponse>>> LoginWithGoogle([FromBody] LoginWithGoogle command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        // Login menggunakan Email dan Password 
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginManualCommand command)
        {
            var result = await _mediator.Send(command); // hasil: { AccessToken, RefreshToken, User }

            _cookieService.SetAuthCookies(Response, result.Data.AccessToken, result.Data.RefreshToken);

            return Ok(new { result.Data }); // Jangan return token ke body!
        }

        // API untuk Register
        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse<AuthResponse>>> Register([FromBody] RegisterCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest("Registration failed");

            return Accepted(result);
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

        
        [HttpGet("me")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] // ← eksplisit
        public async Task<IActionResult> GetCurrentUser(CancellationToken cancellationToken)
            => Ok(await _mediator.Send(new GetCurrentUserQuery(), cancellationToken));



        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh()
        {
            var refreshToken = _cookieService.GetRefreshTokenFromCookie(Request);
            if (string.IsNullOrEmpty(refreshToken))
                return Unauthorized();

            var result = await _mediator.Send(new RefreshTokenCommand(refreshToken));

            _cookieService.SetAuthCookies(Response, result.AccessToken, result.RefreshToken);
            return Ok();
        }

        // Logout
        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var refreshToken = _cookieService.GetRefreshTokenFromCookie(Request);
            if (!string.IsNullOrEmpty(refreshToken))
                await _refreshTokenService.RevokeTokenAsync(refreshToken);

            _cookieService.ClearAuthCookies(Response);
            return Ok();
        }

    }
}