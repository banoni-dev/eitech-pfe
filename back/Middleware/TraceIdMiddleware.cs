using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace EitechPfe.Middleware
{
    public class TraceIdMiddleware
    {
        private readonly RequestDelegate _next;

        public TraceIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.Headers.Append("X-Trace-Id", System.Guid.NewGuid().ToString());
            await _next(context);
        }
    }
}
