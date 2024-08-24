using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;
using ProductApi.Application.Models.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using ProductApi.Infrastructure.Identity;

namespace EmailVerification
{
    class Program
    {
        static void Main(string[] args)
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

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var verificationToken = JsonSerializer.Deserialize<VerificationToken>(message);

                Console.WriteLine($"Received verification token for {verificationToken.Email}");

                using (var scope = BuildServiceProvider().CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<MyIdentityDbContext>();
                    var user = dbContext.Users.SingleOrDefault(u => u.Email == verificationToken.Email);

                    if (user != null)
                    {
                        user.EmailConfirmed = true;
                        dbContext.SaveChanges();
                        Console.WriteLine($"User {user.Email} is now verified.");
                    }
                    else
                    {
                        Console.WriteLine($"User with email {verificationToken.Email} not found.");
                    }
                }
            };
            channel.BasicConsume(queue: "verification_queue",
                                 autoAck: true,
                                 consumer: consumer);

            Console.WriteLine(" Press enter to exit.");
            Console.ReadLine();
        }        
        private static IServiceProvider BuildServiceProvider()
        {
            var services = new ServiceCollection();
            services.AddDbContext<MyIdentityDbContext>(options =>
                options.UseSqlServer("Data Source=.;Initial Catalog=NdTaskIdentity;Integrated Security=true;TrustServerCertificate=True"));

            return services.BuildServiceProvider();
        }
    }
}
