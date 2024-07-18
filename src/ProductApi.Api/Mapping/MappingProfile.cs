using AutoMapper;
using ProductApi.Api.Models;
using ProductApi.Application.Products.Commands;
using ProductApi.Domain.Entities;
using ProductApi.Domain.VM;


namespace ProductApi.Api.Mappings
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
