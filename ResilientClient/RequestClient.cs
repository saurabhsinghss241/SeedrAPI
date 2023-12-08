﻿using Polly;
using Polly.CircuitBreaker;
using Polly.Timeout;
using Polly.Wrap;
using ResilientClient.Intefaces;
using ResilientClient.Models;
using System.Net;

namespace ResilientClient
{
    //For best performance HttpClientWrapper instance lifecycle should be Signleton.
    public class RequestClient : IRequestClient
    {
        private readonly HttpClient _httpClient;
        private readonly AsyncPolicyWrap<HttpResponseMessage> _policyWrap;
        private static readonly string SleepDurationKey = "Broken";
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

            //define policy with retry and circuitbreaker
            //_policyWrap = Done
            _policyWrap = Policy.WrapAsync(GetTimeOutPolicy(timeOutPolicyConfig), GetRetryPolicy(retryPolicyConfig), GetCircuitBreakerPolicy(cbPolicyConfig));
        }


        private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy(CircuitBreakerPolicyConfig cb)
        {
            return Policy<HttpResponseMessage>
                .HandleResult(res => res.StatusCode == HttpStatusCode.InternalServerError)
                .CircuitBreakerAsync(cb.AllowExceptions, TimeSpan.FromSeconds(cb.BreakDuration),
                   onBreak: (dr, ts, ctx) => { ctx[SleepDurationKey] = ts; },
                   onReset: (ctx) => { ctx[SleepDurationKey] = null; });
        }

        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(RetryPolicyConfig rp)
        {
            return Policy<HttpResponseMessage>
            .HandleResult(res => res.StatusCode == HttpStatusCode.InternalServerError)
            //.Or<BrokenCircuitException>()
            .WaitAndRetryAsync(rp.RetryCount,
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

        private static IAsyncPolicy<HttpResponseMessage> GetTimeOutPolicy(TimeOutPolicyConfig to)
        {
            return Policy.TimeoutAsync<HttpResponseMessage>(to.Seconds);
        }
        public async Task<string> GetAsync(string url)
        {
            //var strategy = Policy.WrapAsync(GetRetryPolicy(), GetCircuitBreakerPolicy());
            //HttpResponseMessage response = await strategy.ExecuteAsync(async () => await _httpClient.GetAsync(url));
            HttpResponseMessage response = await _policyWrap.ExecuteAsync(async () => await _httpClient.GetAsync(url));
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
            //var strategy = Policy.WrapAsync(GetRetryPolicy(), GetCircuitBreakerPolicy());
            //HttpResponseMessage response = await strategy.ExecuteAsync(async () => await _httpClient.PostAsync(url, content));
            HttpResponseMessage response = await _policyWrap.ExecuteAsync(async () => await _httpClient.PostAsync(url, content));
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
