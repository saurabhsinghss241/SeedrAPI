using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResilientClient
{
    public interface IHttpClientWrapper
    {
        Task<string> GetAsync(string url);
        Task<string> PostAsync(string url, string content);
        Task<string> PostAsync(string url, FormUrlEncodedContent content);
    }
}
