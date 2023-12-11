using Polly;
using Polly.Wrap;
using ResilientClient.Intefaces;
using ResilientClient.Models;
using System.Net;

namespace ResilientClient
{
    //For best performance HttpClientWrapper instance lifecycle should be Signleton.
    //Using Autofac, add in Program.cs
    //builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    //    var config = builder.Configuration;
    //    builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
    //    {
    //      builder.Register(c => new RequestClient(config.GetSection("LoadTestConfig").Get<RequestClientOptions>())).Keyed<IRequestClient>("LoadTestConfigKey").SingleInstance();
    //      builder.RegisterType<LoadService>().As<ILoadService>().WithAttributeFiltering();
    //    });
    public class RequestClient : IRequestClient
    {
        private readonly HttpClient _httpClient;
        private readonly AsyncPolicyWrap<HttpResponseMessage> _policyWrap;
        private readonly string _baseUrl;
        public string BaseUrl { get { return _baseUrl; } }

        public RequestClient(IRequestClientOptions options)
            : this(options.BaseUrl, options.TimeOutPolicyConfig, options.RetryPolicyConfig, options.CircuitBreakerPolicyConfig)
        {
            _httpClient = new HttpClient();
        }
        public RequestClient(string baseUrl, TimeOutPolicyConfig timeOutPolicyConfig = null, RetryPolicyConfig retryPolicyConfig = null, CircuitBreakerPolicyConfig cbPolicyConfig = null)
        {
            _baseUrl = baseUrl;

            timeOutPolicyConfig ??= new TimeOutPolicyConfig();
            retryPolicyConfig ??= new RetryPolicyConfig();
            cbPolicyConfig ??= new CircuitBreakerPolicyConfig();

            _policyWrap = Policy.WrapAsync(GetTimeOutPolicy(timeOutPolicyConfig), GetRetryPolicy(retryPolicyConfig), GetCircuitBreakerPolicy(cbPolicyConfig));
        }


        private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy(CircuitBreakerPolicyConfig cb)
        {
            return Policy<HttpResponseMessage>
                .HandleResult(res => res.StatusCode == HttpStatusCode.InternalServerError)
                .CircuitBreakerAsync(cb.AllowExceptions, TimeSpan.FromSeconds(cb.BreakDuration),
                   onBreak: (ex, breakDelay, ctx) =>
                   {
                       Console.WriteLine($"Circuit is open due to {ex.Exception.Message}. Retry after {breakDelay.TotalSeconds} seconds.");
                   },
                   onReset: (ctx) =>
                   {
                       Console.WriteLine("Circuit is reset.");
                   },
                   onHalfOpen: () =>
                   {
                       // This is called when the circuit transitions to half-open state
                       Console.WriteLine("Circuit is half-open. Retrying...");
                   });
        }

        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(RetryPolicyConfig rp)
        {
            return Policy<HttpResponseMessage>
            .HandleResult(res => res.StatusCode == HttpStatusCode.InternalServerError)
            //.Or<BrokenCircuitException>()
            .WaitAndRetryAsync(rp.RetryCount,
                sleepDurationProvider: (retryAttempt, ctx) =>
                {
                    //Fixed time
                    //return TimeSpan.FromMilliseconds(200);

                    //exponential backoff strategy
                    return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
                },
                onRetry: (exception, calculatedWaitDuration, retryCount, context) =>
                {
                    Console.WriteLine($"Retry #{retryCount} due to {exception.Exception.Message}. Retrying in {calculatedWaitDuration.TotalSeconds} seconds.");
                });
        }

        private static IAsyncPolicy<HttpResponseMessage> GetTimeOutPolicy(TimeOutPolicyConfig to)
        {
            return Policy.TimeoutAsync<HttpResponseMessage>(to.Seconds);
        }
        public async Task<string> GetAsync(string url)
        {
            HttpResponseMessage response = await _policyWrap.ExecuteAsync(async () => await _httpClient.GetAsync(url));
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> PostAsync(string url, string content)
        {
            HttpResponseMessage response = await _policyWrap.ExecuteAsync(async () => await _httpClient.PostAsync(url, new StringContent(content)));
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> PostAsync(string url, FormUrlEncodedContent content)
        {
            HttpResponseMessage response = await _policyWrap.ExecuteAsync(async () => await _httpClient.PostAsync(url, content));
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
