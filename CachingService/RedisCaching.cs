using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;

namespace CachingService
{
    public class RedisCaching : ICaching
    {
        private readonly ConnectionMultiplexer _connection;
        private readonly IConfiguration _configuration;
        
        public RedisCaching(IConfiguration configuration){
            _configuration = configuration;
            var connectionString = _configuration.GetConnectionString("RedisCache");
            _connection = ConnectionMultiplexer.Connect(connectionString);
        }
        
        public Task<string> GetCache<T>(string key)
        {
            try
            {
                var database = _connection.GetDatabase();
                return database.StringGet(key);
            }
            catch (Exception ex)
            {
                // Handle exceptions (log or throw)
                Console.WriteLine($"Error getting value from Redis Cache: {ex.Message}");
                return null;
            }
        }

        public Task<bool> SaveCache<T>(string key, T data, TimeSpan expiry)
        {
            try
            {
                var database = _connection.GetDatabase();
                return database.StringSet(key, value);
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
