using CachingService.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace CachingService
{
    public class InMemoryCacheService : ICacheService
    {
        private readonly IDistributedCache _cache;
        public InMemoryCacheService(IDistributedCache distributedCache)
        {
            _cache = distributedCache;
        }
        public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class
        {
            try
            {
                string? cachedValue = await _cache.GetStringAsync(key, cancellationToken);

                if (!string.IsNullOrEmpty(cachedValue))
                    return JsonConvert.DeserializeObject<T>(cachedValue);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting value for key {key} from Cache: {ex.Message}");
            }
            return null;
        }

        public async Task RemoveAsync<T>(string key, CancellationToken cancellationToken = default)
        {
            try
            {
                await _cache.RemoveAsync(key, cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing key {key} from Cache: {ex.Message}");
            }
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan expiry, CancellationToken cancellationToken = default) where T : class
        {
            try
            {
                string cacheValue = JsonConvert.SerializeObject(value);

                await _cache.SetStringAsync(key, cacheValue, new DistributedCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(expiry.TotalSeconds)
                }, cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error setting value for key {key} in Redis Cache: {ex.Message}");
            }
        }
    }
}
