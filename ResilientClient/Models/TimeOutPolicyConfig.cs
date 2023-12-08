namespace ResilientClient.Models
{
    public class TimeOutPolicyConfig
    {
        public int AllowExceptions { get; set; } = 3;
        public int BreakDuration { get; set; } = 6;
    }
}
