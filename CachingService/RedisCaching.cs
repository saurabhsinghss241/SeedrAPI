using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace CachingService
{
    public class RedisCaching : ICaching
    {
        private readonly ConnectionMultiplexer _connection;
        private readonly IConfiguration _configuration;

        public RedisCaching(IConfiguration configuration)
        {
            _configuration = configuration;
            string connectionString = _configuration.GetConnectionString("RedisCache");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString), "Redis connection string is null.");
            }
            _connection = ConnectionMultiplexer.Connect(connectionString);
        }

        public async Task<string> GetCache<T>(string key)
        {
            try
            {
                var database = _connection.GetDatabase();
                var result = await database.StringGetAsync(key);
                if (result.HasValue)
                    return result.ToString();

            }
            catch (Exception ex)
            {
                // Handle exceptions (log or throw)
                Console.WriteLine($"Error getting value from Redis Cache: {ex.Message}");
            }
            return string.Empty;
        }

        public async Task<bool> SaveCache<T>(string key, T data, TimeSpan expiry)
        {
            try
            {
                var database = _connection.GetDatabase();
                var json = JsonConvert.SerializeObject(data);
                return await database.StringSetAsync(key, json, TimeSpan.FromMinutes(expiry.TotalMinutes));
            }
            catch (Exception ex)
            {
                // Handle exceptions (log or throw)
                Console.WriteLine($"Error setting value in Redis Cache: {ex.Message}");
                return false;
            }
        }
    }
}
