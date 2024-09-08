using RabbitMQ.Client;
using ProductApi.Application.Interfaces.Messaging;
using ProductApi.Application.Models.Messaging;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using ProductApi.Application.Model.Messaging;

namespace ProductApi.Infrastructure.RabbitMq
{
    public class RabbitMqService : IRabbitMqService
    {
        private readonly RabbitMqSettings _rabbitMqSettings;

        public RabbitMqService(IOptions<RabbitMqSettings> options)
        {
            _rabbitMqSettings = options.Value;
        }
        public void SendVerificationToken(VerificationToken token)
        {
            var factory = new ConnectionFactory()
            {
                HostName = _rabbitMqSettings.HostName,
                Port = _rabbitMqSettings.Port

            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: _rabbitMqSettings.QueueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var message = JsonSerializer.Serialize(token);
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                                 routingKey: _rabbitMqSettings.QueueName,
                                 basicProperties: null,
                                 body: body);
        }
    }
}
