namespace ResilientClient.Models
{
    public class CircuitBreakerPolicyConfig
    {
        public int AllowExceptions { get; set; } = 3;
        public int BreakDuration { get; set; } = 3;
    }
}
