using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace MessageBrokerService
{
    public class RabbitMQService : IMessageBroker
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly int _maxAllowedConsumerCount;
        public RabbitMQService(IMessageBrokerConfig config)
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri(config.ConnectionString)
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _maxAllowedConsumerCount = config.MaxAllowedConsumerCount;
        }
        public void SendMessage(string message, string queueName)
        {
            try
            {
                _channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                var body = Encoding.UTF8.GetBytes(message);

                _channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ReceiveMessage(Action<string> handleMessage, string queueName)
        {
            try
            {
                _channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                var qCount = _channel.ConsumerCount(queue: queueName);
                if (qCount >= _maxAllowedConsumerCount)
                {
                    Console.WriteLine($"Consumer already Running Queue:{queueName} Count:{qCount}");
                    return;
                }

                var consumer = new EventingBasicConsumer(_channel);

                consumer.Received += (model, ea) =>
                {
                    var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                    handleMessage.Invoke(message);
                };

                _channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
