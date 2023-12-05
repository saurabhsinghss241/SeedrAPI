using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CachingService
{
    internal class RedisCaching : ICaching
    {
        public Task<string> GetCache<T>(string key)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveCache<T>(string key, T data, TimeSpan expiry)
        {
            throw new NotImplementedException();
        }
    }
}
