using Frameworks3.Services.Abstractions;
using StackExchange.Redis;

namespace Frameworks3.Services
{
    public class RedisRateLimiter : IRedisRateLimiter
    {
        private readonly IDatabase _context;
        public readonly int _maxRequests;
        private readonly TimeSpan _window;

        public RedisRateLimiter(IConnectionMultiplexer redis, int maxRequests = 10, TimeSpan? window = null)
        {
            _context = redis.GetDatabase();
            _maxRequests = maxRequests;
            _window = window ?? TimeSpan.FromMinutes(1);
        }

        public async Task<bool> AllowRequestAsync(string key)
        {
            var count = await _context.StringIncrementAsync(key);

            if (count == 1)
            {
                await _context.KeyExpireAsync(key, _window);
            }

            return count <= _maxRequests;
        }
    }
}
