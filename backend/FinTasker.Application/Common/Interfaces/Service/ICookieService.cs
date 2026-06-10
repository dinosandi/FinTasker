
namespace FinTasker.Application.Common.Interfaces.Service
{
public interface ICookieService
    {
        void SetAccessTokenCookie(string token);
        void SetRefreshTokenCookie(string token);
        void ClearAuthCookies();
        string? GetAccessToken();
        string? GetRefreshToken();
    }
}

