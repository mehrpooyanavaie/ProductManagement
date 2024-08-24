using AutoMapper;
using ProductApi.Application.ProductsFeatures.Commands.CreateProduct;
using ProductApi.Application.ProductsFeatures.Commands.UpdateProduct;

using ProductApi.Domain.Entities;
using ProductApi.Application.VM;


namespace ProductApi.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateProductCommand, Product>();
            CreateMap<UpdateProductCommand, Product>();
            CreateMap<Product, ProductVM>();
        }
    }
}
