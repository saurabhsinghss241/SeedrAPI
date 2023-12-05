using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace CachingService
{
    public class InMemoryCaching : ICaching
    {
        private readonly IMemoryCache _cache;
        public InMemoryCaching(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }
        public async Task<string> GetCache<T>(string key)
        {
            var result = string.Empty;
            try
            {
                var res = _cache.Get<string>(key);
                if (string.IsNullOrEmpty(res))
                {
                    //NotFound Log
                }
                result = res ?? string.Empty;
            }
            catch (Exception)
            {
                //log
                //throw;
            }
            return result;
        }

        public async Task<bool> SaveCache<T>(string key, T data, TimeSpan expiry)
        {
            var result = true;
            try
            {
                var jsondata = JsonConvert.SerializeObject(data);
                _cache.Set<string>(key, jsondata, TimeSpan.FromMinutes(expiry.TotalMinutes));
            }
            catch (Exception)
            {
                result = false;
                //Log error
                //throw;
            }
            return result;
        }
    }
}
