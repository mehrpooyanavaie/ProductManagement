using AutoMapper;
using ProductApi.Application.ProductsFeatures.Commands.CreateProduct;
using ProductApi.Application.ProductsFeatures.Commands.UpdateProduct;
using ProductApi.Application.CategoriesFeatures.Commands.CreateCategory;
using ProductApi.Application.DTOs;
using ProductApi.Domain.Entities;
using ProductApi.Application.VM;
using ProductApi.Application.CategoriesFeatures.Commands.UpdateCategory;


namespace ProductApi.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateProductCommand, Product>();

            CreateMap<UpdateProductCommand, Product>();

            CreateMap<Product, ProductVM>()
                .ForMember(dest => dest.CategoryNames, opt => opt.
                    MapFrom(src => src.categoryProducts.Select(cp => cp.category.Name)));

            CreateMap<Product, RedisProductDTO>()
                .ForMember(dest => dest.CategoryIds, opt => opt.
                    MapFrom(src => src.categoryProducts.Select(cp => cp.category.Id)));

            CreateMap<RedisProductDTO, Product>()
                 .ForMember(dest => dest.categoryProducts, opt => opt.MapFrom(src =>
                     src.CategoryIds.Select(id => new CategoryProduct { CategoryId = id })));

            CreateMap<RedisProductDTO, ProductVM>();

            CreateMap<CreateCategoryCommand, Category>();

            CreateMap<CreateProductCommand, RedisProductDTO>();

            CreateMap<UpdateProductCommand, RedisProductDTO>();

            CreateMap<Category, CategoryVM>();

            CreateMap<UpdateCategoryCommand, Category>();
        }
    }
}
