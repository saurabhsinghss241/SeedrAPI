namespace MessageBrokerService
{
    public class RabbitMQConfig : IMessageBrokerConfig
    {
        public string ConnectionString { get; set; }
        public int MaxAllowedConsumerCount { get; set; } = 1;
    }
}
