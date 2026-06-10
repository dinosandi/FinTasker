using MediatR;
using FinTasker.Application.Common.Models;
using FinTasker.Application.Features.Auth.Commands.RefreshToken;
using FinTasker.Application.Common.Interfaces.Service;

namespace FinTasker.Application.Features.Auth.Commands.RefreshToken
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, AuthResponse>
    {
        private readonly ICookieService       _cookieService;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IJwtService          _jwtService;

        public RefreshTokenHandler(
            ICookieService cookieService,
            IRefreshTokenService refreshTokenService,
            IJwtService jwtService)
        {
            _cookieService       = cookieService;
            _refreshTokenService = refreshTokenService;
            _jwtService          = jwtService;
        }

        public async Task<AuthResponse> Handle(
            RefreshTokenCommand request,
            CancellationToken cancellationToken)
        {
            // 1. Ambil refresh token dari cookie
            var token = _cookieService.GetRefreshToken()
                ?? throw new UnauthorizedAccessException("Refresh token tidak ditemukan.");

            // 2. Validasi token di database
            var refreshToken = await _refreshTokenService.GetValidTokenAsync(token)
                ?? throw new UnauthorizedAccessException("Refresh token tidak valid atau sudah expired.");

            // 3. Rotate: revoke yang lama, buat yang baru (best practice)
            await _refreshTokenService.RevokeTokenAsync(token);
            var newRefreshToken = await _refreshTokenService
                .GenerateRefreshTokenAsync(refreshToken.UsersId);

            // 4. Generate access token baru
            var newAccessToken = _jwtService.GenerateToken(refreshToken.Users);

            // 5. Set cookie baru
            _cookieService.SetAccessTokenCookie(newAccessToken);
            _cookieService.SetRefreshTokenCookie(newRefreshToken.Token);

            return new AuthResponse
            {
                UserId = refreshToken.UsersId,
                Email  = refreshToken.Users.Email,
                Name   = refreshToken.Users.Name
            };
        }
    }
}
