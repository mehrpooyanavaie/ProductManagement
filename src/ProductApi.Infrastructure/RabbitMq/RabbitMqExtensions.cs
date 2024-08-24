using Microsoft.Extensions.DependencyInjection;
using ProductApi.Application.Interfaces.Messaging;
namespace ProductApi.Infrastructure.RabbitMq
{
    public static class RabbitMqExtensions
    {
            public static IServiceCollection ConfigureRabbitMqServices(this IServiceCollection services)
            {
                services.AddTransient<IRabbitMqService, RabbitMqService>();
                return services;
            }
                         
    }
}