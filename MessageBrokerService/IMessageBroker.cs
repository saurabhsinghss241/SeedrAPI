namespace MessageBrokerService
{
    public interface IMessageBroker
    {
        void SendMessage(string message, string queueName);
        void ReceiveMessage(Action<string> handleMessage, string queueName);
    }
}
