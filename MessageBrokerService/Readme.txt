# Program.cs 
builder.Services.AddSingleton<IMessageBroker, RabbitMQService>(sp => new RabbitMQService(config.GetSection("RabbitMQConfig").Get<RabbitMQConfig>()));

# appsettings.json
"RabbitMQConfig": {
    "ConnectionString": "amqps://username:password@lionfish.rmq.cloudamqp.com/dbname",
    "MaxAllowedConsumerCount": 1
}


# Singleton => To avoid connection exhaustion issue, reuse same connection.
# MaxAllowedConsumerCount => prevents connection exhaustion for consumer request.