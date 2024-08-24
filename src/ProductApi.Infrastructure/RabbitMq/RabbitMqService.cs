using RabbitMQ.Client;
using ProductApi.Application.Interfaces.Messaging;
using ProductApi.Application.Models.Messaging;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;

namespace ProductApi.Infrastructure.RabbitMq
{
        public class RabbitMqService : IRabbitMqService
    {

        public void SendVerificationToken(VerificationToken token)
        {
            var factory = new ConnectionFactory() 
            { 
                HostName = "localhost", 
                Port = 5672
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "verification_queue",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var message = JsonSerializer.Serialize(token);
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                                 routingKey: "verification_queue",
                                 basicProperties: null,
                                 body: body);
        }
    }
}
