using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace CachingService
{
    //Register in Program.cs
    //builder.Services.AddDistributedMemoryCache();
    //builder.Services.AddSignleton<ICacheService,InMemoryCacheService>();
    public class InMemoryCacheService : ICacheService
    {
        private readonly IDistributedCache _cache;
        public InMemoryCacheService(IDistributedCache distributedCache)
        {
            _cache = distributedCache;
        }
        public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class
        {
            string? cachedValue = await _cache.GetStringAsync(key, cancellationToken);

            if (string.IsNullOrEmpty(cachedValue))
                return null;

            T? value = JsonConvert.DeserializeObject<T>(cachedValue);
            return value;
        }

        public async Task RemoveAsync<T>(string key, CancellationToken cancellationToken = default)
        {
            await _cache.RemoveAsync(key, cancellationToken);
        }

        public async Task SetAsync<T>(string key, T value, CancellationToken cancellationToken = default) where T : class
        {
            string cacheValue = JsonConvert.SerializeObject(value);

            await _cache.SetStringAsync(key, cacheValue, cancellationToken);
        }
    }
}
