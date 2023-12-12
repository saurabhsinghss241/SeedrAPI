using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace CachingService
{
    //Register in Program.cs
    //builder.Services.AddSignleton<ICacheService,RedisCacheService>();
    public class RedisCacheService : ICacheService
    {
        private readonly ConnectionMultiplexer _connection;
        private readonly IConfiguration _configuration;

        public RedisCacheService(IConfiguration configuration)
        {
            _configuration = configuration;
            string connectionString = _configuration.GetConnectionString("RedisCache");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString), "Redis connection string is null.");
            }
            _connection = ConnectionMultiplexer.Connect(connectionString);
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
