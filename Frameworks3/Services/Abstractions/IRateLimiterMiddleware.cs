namespace Frameworks3.Services.Abstractions
{
    public interface IRateLimiterMiddleware
    {
        Task InvokeAsync(HttpContext context);
    }
}