using MediatR;
using Google.Apis.Auth;
using Microsoft.EntityFrameworkCore;
using FinTasker.Application.Common.Models;
using FinTasker.Domain.Entities;
using FinTasker.Domain.Enums;
using Microsoft.Extensions.Configuration;
using FinTasker.Application.Common.Interfaces;
using FinTasker.Application.Common.Interfaces.Service;
using FinTasker.Application.Common.Exceptions;

namespace FinTasker.Application.Features.Auth.Commands.LoginWithGoogle
{
    public class LoginHandler : IRequestHandler<LoginWithGoogle, ApiResponse<AuthResponse>>
    {
        private readonly IAuthenticationService _authService;
        private readonly ICookieService _cookieService;
        private readonly IRefreshTokenService _refreshTokenService;


        public LoginHandler(IAuthenticationService authService, ICookieService cookieService, IRefreshTokenService refreshTokenService)
        {
            _authService         = authService;
            _cookieService       = cookieService;
            _refreshTokenService = refreshTokenService;
        }

        public async Task<ApiResponse<AuthResponse>> Handle(LoginWithGoogle request, CancellationToken cancellationToken)
        {
            AuthResult authResult = await _authService
                .AuthenticateWithGoogleAsync(request.IdToken, cancellationToken);

            var refreshToken = await _refreshTokenService
                .GenerateRefreshTokenAsync(authResult.UserId);

            Console.WriteLine($"userId: {authResult.Email} with goole"); 

            _cookieService.SetAccessTokenCookie(authResult.AccessToken);
            _cookieService.SetRefreshTokenCookie(refreshToken.Token);

            var response = new AuthResponse
            {
                UserId = authResult.UserId,
                Email = authResult.Email,
                Name = authResult.Name
            };
            
            return ApiResponse<AuthResponse>.Ok(response, "Login successful.");
        }
    }
}