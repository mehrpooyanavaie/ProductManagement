using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductApi.Application.Interfaces;
namespace ProductApi.Infrastructure.UnitOfWork
{
    public static class UnitOfWorkServiceExtensions
    {
            public static IServiceCollection ConfigureUnitOfWorkService(this IServiceCollection services)
            {
                services.AddTransient<IUnitOfWork, UnitOfWork>();
                return services;
            }

    }
}
