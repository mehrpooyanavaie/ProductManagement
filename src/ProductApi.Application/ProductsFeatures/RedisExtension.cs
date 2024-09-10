//    services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(Configuration.GetConnectionString("RedisConnection")));
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductApi.Application.Interfaces.Messaging;
using ProductApi.Application.Model.Messaging;
using StackExchange.Redis;
namespace ProductApi.Application.ProductsFeatures
{
    public static class ReddisExtension
    {
        public static IServiceCollection ConfigureRedisServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSingleton<IConnectionMultiplexer>
                (ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection")));
            return services;
        }

    }
}