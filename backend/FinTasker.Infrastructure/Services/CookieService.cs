using System;
using Microsoft.AspNetCore.Http;
using FinTasker.Application.Common.Interfaces.Service;
using Microsoft.Extensions.Configuration;

namespace FinTasker.Infrastructure.Services
{
    public class CookieService : ICookieService
    {
        private readonly IConfiguration _config;

        // Nama cookie
        private const string AccessTokenCookie = "access_token";
        private const string RefreshTokenCookie = "refresh_token";

        public CookieService(IConfiguration config)
        {
            _config = config;
        }

        public void SetAuthCookies(HttpResponse response, string accessToken, string refreshToken)
        {
            var isProduction = !string.Equals(
                _config["ASPNETCORE_ENVIRONMENT"], "Development",
                StringComparison.OrdinalIgnoreCase
            );

            var accessTokenExpire = int.Parse(_config["Jwt:ExpireMinutes"] ?? "60");
            var refreshTokenExpireDays = int.Parse(_config["Jwt:RefreshExpireDays"] ?? "7");

            // Cookie untuk Access Token (JWT)
            var accessCookieOptions = new CookieOptions
            {
                HttpOnly = true,            
                Secure = isProduction,      // HTTPS only di production, false di development
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddMinutes(accessTokenExpire),
                Path = "/"
            };

            // Cookie untuk Refresh Token — expiry lebih panjang
            var refreshCookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = isProduction,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddDays(refreshTokenExpireDays),
                Path = "/api/auth"          // Batasi path — hanya dikirim ke endpoint auth
            };

            response.Cookies.Append(AccessTokenCookie, accessToken, accessCookieOptions);
            response.Cookies.Append(RefreshTokenCookie, refreshToken, refreshCookieOptions);
        }

        public void ClearAuthCookies(HttpResponse response)
        {
            response.Cookies.Delete(AccessTokenCookie, new CookieOptions { Path = "/" });
            response.Cookies.Delete(RefreshTokenCookie, new CookieOptions { Path = "/api/auth" });
        }

        public string? GetRefreshTokenFromCookie(HttpRequest request)
        {
            return request.Cookies[RefreshTokenCookie];
        }
    }
}


