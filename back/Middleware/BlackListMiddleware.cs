namespace EitechPfe.Middleware
{
    public class BlacklistMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IBlackListedService _blackListedService;

        public BlacklistMiddleware(RequestDelegate next, IBlackListedService blackListedService)
        {
            _next = next;
            _blackListedService = blackListedService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var ipAddress = context.Connection.RemoteIpAddress?.ToString();
            if (ipAddress != null)
            {
                // var isBlacklisted = await _blackListedService.IsIpBlacklisted(ipAddress);
                // if (isBlacklisted)
                // {
                //     context.Response.StatusCode = StatusCodes.Status403Forbidden;
                //     await context.Response.WriteAsync("Access denied. Your IP is blacklisted.");
                //     return;
                // }
            }

            await _next(context);
        }
    }
}