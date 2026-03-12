using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace UserManagementAPI.Middleware
{
    // Define the middleware class
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        // This points to the next middleware in the process.

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
            // Save the next middleware; to be called later.
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Log request details BEFORE passing it on
            Console.WriteLine($"Reuest: {context.Request.Method} {context.Request.Path}");

            await _next(context);
            // Pass control to the next middleware

            // Log response details AFTER completion of the next middleware
            Console.WriteLine($"Response: {context.Response.StatusCode}");
        }
    }

    // Register this in Program.cs to call this service
    public static class LoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseLoggingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoggingMiddleware>();
        }
    }
}