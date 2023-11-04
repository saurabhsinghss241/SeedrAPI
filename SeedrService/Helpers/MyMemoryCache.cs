using Microsoft.Extensions.Caching.Memory;

namespace SeedrService.Helpers
{
    public class MyMemoryCache
    {
        public MemoryCache Cache { get; } = new MemoryCache(
            new MemoryCacheOptions
            {
                SizeLimit = 1024
            });
    } 
}

