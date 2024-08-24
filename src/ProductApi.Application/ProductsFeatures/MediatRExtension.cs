using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Runtime.InteropServices;

namespace ProductApi.Application.ProductsFeatures
{
    public static class MediatRExtension
    {
        public static IServiceCollection ConfigurMediatRServices(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly(),
                 typeof(ProductApi.Application.ProductsFeatures.Commands.CreateProduct.CreateProductCommand).Assembly);
            return services;
        }
    }
}