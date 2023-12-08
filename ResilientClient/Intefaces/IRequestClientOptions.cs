using ResilientClient.Models;

namespace ResilientClient.Intefaces
{
    public interface IRequestClientOptions
    {
        string BaseUrl { get; set; }
        TimeOutPolicyConfig TimeOutPolicyConfig { get; set; }
        RetryPolicyConfig RetryPolicyConfig { get; set; }
        CircuitBreakerPolicyConfig CircuitBreakerPolicyConfig { get; set; }
    }
}
