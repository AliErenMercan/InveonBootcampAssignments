using StackExchange.Redis;
using System.Text.Json;

namespace InveonBootcamp.AssignmentW1.BestPracticesAPI.Services
{
    public class CachingService
    {
        private readonly IConnectionMultiplexer _redis;

        public CachingService(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan expiration)
        {
            var db = _redis.GetDatabase();
            var jsonData = JsonSerializer.Serialize(value);
            await db.StringSetAsync(key, jsonData, expiration);
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var db = _redis.GetDatabase();
            var jsonData = await db.StringGetAsync(key);

            if (jsonData.IsNullOrEmpty) return default;

            return JsonSerializer.Deserialize<T>(jsonData);
        }
    }
}
