using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductApi.Application.Interfaces.Messaging;
using ProductApi.Application.Model.Messaging;
namespace ProductApi.Infrastructure.RabbitMq
{
    public static class RabbitMqExtensions
    {
        public static IServiceCollection ConfigureRabbitMqServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<RabbitMqSettings>(configuration.GetSection("RabbitMq"));
            services.AddTransient<IRabbitMqService, RabbitMqService>();
            return services;
        }

    }
}