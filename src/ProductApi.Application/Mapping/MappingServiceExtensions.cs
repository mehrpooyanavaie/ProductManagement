using Microsoft.Extensions.DependencyInjection;
namespace ProductApi.Application.Mapping
{
    public static class MappingServiceExtensions
{
    public static IServiceCollection ConfigureMappingService(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingServiceExtensions).Assembly);
        return services;
    }
}
}
