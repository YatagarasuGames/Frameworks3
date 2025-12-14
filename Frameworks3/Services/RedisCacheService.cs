using Frameworks3.Services.Abstractions;
using StackExchange.Redis;
using System.Text.Json;

namespace Frameworks3.Services
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IDatabase _context;
        private readonly JsonSerializerOptions _jsonOptions;

        public RedisCacheService(IConnectionMultiplexer redis)
        {
            _context = redis.GetDatabase();

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false
            };
        }

        public async Task<T?> GetCachedAsync<T>(string key)
        {
            var cached = await _context.StringGetAsync(key);

            if (cached.IsNullOrEmpty)
                return default;

            return JsonSerializer.Deserialize<T>(cached!, _jsonOptions);
        }

        public async Task SetCachedAsync<T>(string key, T data, int ttlSeconds)
        {
            var json = JsonSerializer.Serialize(data, _jsonOptions);

            if (ttlSeconds > 0)
            {
                await _context.StringSetAsync(
                    key,
                    json,
                    TimeSpan.FromSeconds(ttlSeconds)
                );
            }
            else
            {
                await _context.StringSetAsync(key, json);
            }
        }
    }
}
