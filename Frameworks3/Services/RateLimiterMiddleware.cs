using Frameworks3.Services.Abstractions;

namespace Frameworks3.Services
{
    public class RateLimiterMiddleware : IRateLimiterMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly RedisRateLimiter _limiter;

        public RateLimiterMiddleware(RequestDelegate next, RedisRateLimiter limiter)
        {
            _next = next;
            _limiter = limiter;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
            var key = $"ratelimit:{ip}";

            var allowed = await _limiter.AllowRequestAsync(key);
            context.Response.Headers["X-RateLimit-Limit"] = _limiter._maxRequests.ToString();

            if (!allowed)
            {
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                await context.Response.WriteAsync("Слишком много запросов, попробуйте позже.");
                return;
            }

            await _next(context);
        }
    }
}
