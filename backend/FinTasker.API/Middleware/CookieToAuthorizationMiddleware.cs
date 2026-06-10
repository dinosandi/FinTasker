using System;

namespace FinTasker.API.Middleware
{
    public class CookieToAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private const string AccessTokenKey = "access_token";

        public CookieToAuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Cookies.TryGetValue(AccessTokenKey, out var token)
                && !string.IsNullOrEmpty(token)
                && !context.Request.Headers.ContainsKey("Authorization"))
            {
                context.Request.Headers.Append(
                    "Authorization", $"Bearer {token}");
            }

            await _next(context);
        }
    }
}

