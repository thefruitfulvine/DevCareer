using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagementAPI.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value?.ToLower();

            // Let Swagger and static files without authentication
            if (path.StartsWith("/swagger") || path.StartsWith("/index.html") || path.StartsWith("/favicon.ico"))
            {
                await _next(context);
                return;
            }

            // Extract Authorization header from the request
            var token = context.Request.Headers["Authorization"].FirstOrDefault();

            if (string.IsNullOrEmpty(token) || token != "Bearer valid-token")
            {
                // Block access if token is invalid or null
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized");
                return;
            }

            await _next(context);
            // Token is valid, continue the next middleware
        }
    }

    public static class AuthenticationMiddlewareExtension
    {
        public static IApplicationBuilder UseAuthenticationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthenticationMiddleware>();
        }
    }
}