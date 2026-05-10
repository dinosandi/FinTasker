using System;
using Microsoft.AspNetCore.Http;

namespace FinTasker.Application.Common.Interfaces.Service
{
    public interface ICookieService
    {
        void SetAuthCookies(HttpResponse response, string accessToken, string refreshToken);
        void ClearAuthCookies(HttpResponse response);
        string? GetRefreshTokenFromCookie(HttpRequest request);
    }

}

