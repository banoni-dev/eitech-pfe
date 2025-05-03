using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace EitechPfe.Middleware
{
    public class DeviceFingerprintMiddleware
    {
        private readonly RequestDelegate _next;

        public DeviceFingerprintMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Add logic to handle device fingerprinting
            await _next(context);
        }
    }
}
