namespace MessageBrokerService
{
    public interface IMessageBrokerConfig
    {
        string ConnectionString { get; set; }
        int MaxAllowedConsumerCount { get; set; }
    }
}
