namespace Frameworks3.Services.Abstractions
{
    public interface IRedisRateLimiter
    {
        Task<bool> AllowRequestAsync(string key);
    }
}