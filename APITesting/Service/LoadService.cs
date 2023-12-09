
using Autofac.Features.AttributeFilters;
using ResilientClient.Intefaces;

namespace APITesting.Service
{
    public class LoadService : ILoadService
    {
        private readonly IRequestClient _requestClient;
        public LoadService([KeyFilter("LoadTestConfigKey")] IRequestClient requestClient)
        {
            _requestClient = requestClient;
        }
        public async Task<string> GetDataNew(int statusCode)
        {
            var url = $"{_requestClient.BaseUrl}{statusCode}";
            return await _requestClient.GetAsync(url);
        }

        public Task<string> GetDataOld(int statusCode)
        {
            throw new NotImplementedException();
        }
    }
}
