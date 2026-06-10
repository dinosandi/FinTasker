using FinTasker.Application.Common.Interfaces.Service;
using FinTasker.Application.Common.Models;
using MediatR;

namespace FinTasker.Application.Features.Auth.Commands.LoginManualWithEmail
{
    public class LoginManualHandler
        : IRequestHandler<LoginManualCommand, ApiResponse<AuthResponse>>
    {
        private readonly IAuthenticationService _authService;
        private readonly ICookieService         _cookieService;
        private readonly IRefreshTokenService   _refreshTokenService;

        public LoginManualHandler(
            IAuthenticationService authService,
            ICookieService cookieService,
            IRefreshTokenService refreshTokenService)
        {
            _authService         = authService;
            _cookieService       = cookieService;
            _refreshTokenService = refreshTokenService;
        }

        public async Task<ApiResponse<AuthResponse>> Handle(
            LoginManualCommand request,
            CancellationToken cancellationToken)
        {
            // 1. Dapat AuthResult (internal) — berisi AccessToken
            AuthResult authResult = await _authService
                .AuthenticateAsync(request.Email, request.Password);

            // 2. Generate refresh token
            var refreshToken = await _refreshTokenService
                .GenerateRefreshTokenAsync(authResult.UserId);

            // 3. Simpan ke HttpOnly cookie — tidak expose ke client
            _cookieService.SetAccessTokenCookie(authResult.AccessToken);
            _cookieService.SetRefreshTokenCookie(refreshToken.Token);

            // 4. Map ke AuthResponse (client model) — tanpa token
            var response = new AuthResponse
            {
                UserId = authResult.UserId,
                Email  = authResult.Email,
                Name   = authResult.Name
            };

            return ApiResponse<AuthResponse>.SuccessResponse(response, "Login berhasil.");
        }
    }
}