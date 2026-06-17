using FinTasker.Application.Common.Interfaces.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace FinTasker.Infrastructure.Services
{
    public class CookieService : ICookieService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _config;

        private const string AccessTokenKey  = "access_token";
        private const string RefreshTokenKey = "refresh_token";

        public CookieService(
            IHttpContextAccessor httpContextAccessor,
            IConfiguration config)
        {
            _httpContextAccessor = httpContextAccessor;
            _config              = config;
        }

        private HttpContext HttpContext =>
            _httpContextAccessor.HttpContext
            ?? throw new InvalidOperationException(
                "CookieService tidak bisa digunakan di luar HTTP request context.");

        // ── SET 

        public void SetAccessTokenCookie(string token)
        {
            var expiryMinutes = int.Parse(
                _config["Jwt:ExpiryMinutes"] ?? "15");

            HttpContext.Response.Cookies.Append(
                AccessTokenKey,
                token,
                BuildCookieOptions(
                    DateTimeOffset.UtcNow.AddMinutes(expiryMinutes)));
        }

        public void SetRefreshTokenCookie(string token)
        {
            var expiryDays = int.Parse(
                _config["Jwt:RefreshExpireDays"] ?? "30");

            // Path dibatasi ke /api/auth/refresh saja
            // Browser tidak kirim cookie ini ke endpoint lain → minimize exposure
            HttpContext.Response.Cookies.Append(
                RefreshTokenKey,
                token,
                BuildCookieOptions(
                    DateTimeOffset.UtcNow.AddDays(expiryDays),
                    path: "/api/auth/refresh"));
        }

        // ── GET 

        public string? GetAccessToken()
            => HttpContext.Request.Cookies[AccessTokenKey];

        public string? GetRefreshToken()
            => HttpContext.Request.Cookies[RefreshTokenKey];

        // ── CLEAR 

        public void ClearAuthCookies()
        {
            // Expires masa lalu → browser hapus cookie secara otomatis
            HttpContext.Response.Cookies.Append(
                AccessTokenKey, "",
                BuildCookieOptions(DateTimeOffset.UtcNow.AddDays(-1)));

            HttpContext.Response.Cookies.Append(
                RefreshTokenKey, "",
                BuildCookieOptions(
                    DateTimeOffset.UtcNow.AddDays(-1),
                    path: "/api/auth/refresh"));
        }

        // ── PRIVATE HELPER

        private bool IsProduction =>
            _config["ASPNETCORE_ENVIRONMENT"] == "Production";

        private CookieOptions BuildCookieOptions(
            DateTimeOffset expires,
            string path = "/")
        {
            return new CookieOptions
            {
                HttpOnly = true,
                // Secure=true di Production (HTTPS), false di Development (HTTP local)
                Secure   = false,
                SameSite = SameSiteMode.None,
                Expires  = expires,
                Path     = path
            };
        }
    }
}
