using CachingService.Interfaces;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace CachingService
{
    public class RedisCacheService : ICacheService
    {
        private readonly ConnectionMultiplexer _connection;
        public RedisCacheService(IRedisCacheConfig config)
        {
            _connection = ConnectionMultiplexer.Connect($"{config.HostName}:{config.Port},user={config.Username},password={config.Password}");
        }
        public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class
        {
            try
            {
                var database = _connection.GetDatabase();
                var result = await database.StringGetAsync(key);
                if (result.HasValue)
                    return JsonConvert.DeserializeObject<T>(result.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting value for key {key} from Redis Cache: {ex.Message}");
            }
            return null;
        }

        public async Task RemoveAsync<T>(string key, CancellationToken cancellationToken = default)
        {
            try
            {
                var database = _connection.GetDatabase();
                await database.KeyDeleteAsync(key);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing key {key} from Redis Cache: {ex.Message}");
            }
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan expiry, CancellationToken cancellationToken = default) where T : class
        {
            try
            {
                var database = _connection.GetDatabase();
                string cacheValue = JsonConvert.SerializeObject(value);

                await database.StringSetAsync(key, cacheValue, TimeSpan.FromSeconds(expiry.TotalSeconds));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error setting value for key {key} in Redis Cache: {ex.Message}");
            }
        }
    }
}
