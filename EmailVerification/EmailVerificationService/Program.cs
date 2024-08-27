using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;

namespace EmailVerificationService
{
    public class Program
    {
        private const string HostName = "localhost";
        private const string QueueName = "verification_queue";

        public static void Main(string[] args)
        {
            var factory = new ConnectionFactory() 
            { 
                HostName = HostName,
                Port = 5672
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: QueueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var verificationToken = JsonConvert.DeserializeObject<VerificationToken>(message);

                    Console.WriteLine($"Email: {verificationToken.Email}");
                    Console.WriteLine($"Token: {verificationToken.Token}");
                    Console.WriteLine($"Expiry Date: {verificationToken.ExpiryDate}");
                    Console.WriteLine();
                };

                channel.BasicConsume(queue: QueueName,
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine("Press enter to exit.");
                Console.ReadLine(); 
            }
        }
    }

    public class VerificationToken
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
