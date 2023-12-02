using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;
using System.Net;

namespace ResilientClient
{
    public class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly HttpClient _httpClient;
        //private readonly AsyncRetryPolicy _retryPolicy;
        //private readonly AsyncCircuitBreakerPolicy<string> _circuitBreakerPolicy;
        private static readonly string SleepDurationKey = "Broken";
        public HttpClientWrapper(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return Policy<HttpResponseMessage>
                .HandleResult(res => res.StatusCode == HttpStatusCode.InternalServerError)
                .CircuitBreakerAsync(3, TimeSpan.FromSeconds(2),
                   onBreak: (dr, ts, ctx) => { ctx[SleepDurationKey] = ts; },
                   onReset: (ctx) => { ctx[SleepDurationKey] = null; });
        }

        static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return Policy<HttpResponseMessage>
            .HandleResult(res => res.StatusCode == HttpStatusCode.InternalServerError)
            .Or<BrokenCircuitException>()
            .WaitAndRetryAsync(4,
                sleepDurationProvider: (c, ctx) =>
                {
                    if (ctx.ContainsKey(SleepDurationKey))
                        return (TimeSpan)ctx[SleepDurationKey];
                    return TimeSpan.FromMilliseconds(200);
                },
                onRetry: (dr, ts, ctx) =>
                {
                    Console.WriteLine($"Context: {(ctx.ContainsKey(SleepDurationKey) ? "Open" : "Closed")}");
                    Console.WriteLine($"Waits: {ts.TotalMilliseconds}");
                });
        }
        public async Task<string> GetAsync(string url)
        {
            var strategy = Policy.WrapAsync(GetRetryPolicy(), GetCircuitBreakerPolicy());
            HttpResponseMessage response = await strategy.ExecuteAsync(async () => await _httpClient.GetAsync(url));
            //var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> PostAsync(string url, string content)
        {
            HttpResponseMessage response = await _httpClient.PostAsync(url, new StringContent(content));
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> PostAsync(string url, FormUrlEncodedContent content)
        {
            var strategy = Policy.WrapAsync(GetRetryPolicy(), GetCircuitBreakerPolicy());
            HttpResponseMessage response = await strategy.ExecuteAsync(async () => await _httpClient.PostAsync(url, content));
            //HttpResponseMessage response = await _httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
