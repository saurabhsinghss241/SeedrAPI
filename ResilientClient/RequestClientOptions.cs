using ResilientClient.Intefaces;
using ResilientClient.Models;

namespace ResilientClient
{
    public class RequestClientOptions : IRequestClientOptions
    {
        public string BaseUrl { get; set; }
        public TimeOutPolicyConfig TimeOutPolicyConfig { get; set; }
        public RetryPolicyConfig RetryPolicyConfig { get; set; }
        public CircuitBreakerPolicyConfig CircuitBreakerPolicyConfig { get; set; }
    }
}
