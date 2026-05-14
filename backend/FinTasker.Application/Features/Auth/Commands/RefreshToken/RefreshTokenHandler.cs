using MediatR;
using FinTasker.Application.Common.Interfaces.Service;
using FinTasker.Application.Common.Models;
using FinTasker.Application.Common.Exceptions;

namespace FinTasker.Application.Features.Auth.Commands.RefreshToken
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, AuthResponse>
    {
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IJwtService _jwtService;

        public RefreshTokenHandler(IRefreshTokenService refreshTokenService, IJwtService jwtService)
        {
            _refreshTokenService = refreshTokenService;
            _jwtService = jwtService;
        }

        public async Task<AuthResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            // Validasi refresh token dari database
            var existingToken = await _refreshTokenService.GetValidTokenAsync(request.Token);

            if (existingToken == null)
                throw new BadRequestException("Refresh token tidak valid atau sudah expired.");

            // Revoke token lama (rotation — satu token hanya bisa dipakai sekali)
            await _refreshTokenService.RevokeTokenAsync(request.Token);

            // Generate token baru
            var newAccessToken = _jwtService.GenerateToken(existingToken.Users);
            var newRefreshToken = await _refreshTokenService.GenerateRefreshTokenAsync(existingToken.UsersId);

            return new AuthResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken.Token
            };
        }
    }
}