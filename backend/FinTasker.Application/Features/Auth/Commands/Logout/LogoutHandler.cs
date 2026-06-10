using FinTasker.Application.Common.Interfaces.Service;
using MediatR;

namespace FinTasker.Application.Features.Auth.Commands.Logout
{
    public class LogoutHandler : IRequestHandler<LogoutCommand>
    {
        private readonly ICookieService       _cookieService;
        private readonly IRefreshTokenService _refreshTokenService;

        public LogoutHandler(
            ICookieService cookieService,
            IRefreshTokenService refreshTokenService)
        {
            _cookieService       = cookieService;
            _refreshTokenService = refreshTokenService;
        }

        public async Task Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            var token = _cookieService.GetRefreshToken();

            // Revoke di DB jika ada — logout dari semua device bisa pakai RevokeAllUserTokens
            if (token is not null)
                await _refreshTokenService.RevokeTokenAsync(token);

            // Hapus cookie dari browser
            _cookieService.ClearAuthCookies();
        }
    }
}

