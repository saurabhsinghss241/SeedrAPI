
using Autofac.Features.AttributeFilters;
using CachingService.Interfaces;
using ResilientClient.Intefaces;

namespace APITesting.Service
{
    public class LoadService : ILoadService
    {
        private readonly IRequestClient _requestClient;
        private readonly ICacheService _cache;
        public LoadService([KeyFilter("LoadTestConfigKey")] IRequestClient requestClient, ICacheService cache)
        {
            _requestClient = requestClient;
            _cache = cache;
        }
        public async Task<string> GetDataNew(int statusCode)
        {
            var url = $"{_requestClient.BaseUrl}{statusCode}";

            var cacheResult = await _cache.GetAsync<string>(url);
            if (string.IsNullOrEmpty(cacheResult))
            {
                var res = await _requestClient.GetAsync(url);
                await _cache.SetAsync<string>(url, res, TimeSpan.FromMinutes(5));
                return res;
            }
            return cacheResult;
        }

        public Task<string> GetDataOld(int statusCode)
        {
            throw new NotImplementedException();
        }
    }
}
