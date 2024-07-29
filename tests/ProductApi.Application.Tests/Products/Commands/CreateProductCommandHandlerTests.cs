using AutoMapper;
using ProductApi.Application.Interfaces;
using ProductApi.Domain.VM;
using ProductApi.Domain.Entities;
using ProductApi.Application.Products.Commands;
using ProductApi.Application.Products.Queries;
using ProductApi.Api.Mappings;
using ProductApi.Application.Tests.Mocks;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Application.Tests.Products.Commands
{
    public class CreateProductCommandHandlerTests
    {
        IMapper _mapper;
        Mock<IUnitOfWork> _mockRepository;
        CreateProductCommand _CreateProductDto;
        public CreateProductCommandHandlerTests()
        {
            _mockRepository = MockProductRepository.GetProductRepository();

            var mapperConfig = new MapperConfiguration(m =>
            {
                m.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _CreateProductDto = new CreateProductCommand()
            {
                ManufactureEmail = "mnavaienezhad@gmail.com",
                ManufacturePhone = "09129994444",
                ProduceDate = new DateTime(2012, 12, 25, 10, 30, 50),
                UserId = "mpnv168",
                Name = "Test DTO"
            };
        }

        [Fact]
        public async Task CreateProductType()
        {
            var handler = new CreateProductCommandHandler(_mockRepository.Object, _mapper);
            var result = await handler.Handle(new CreateProductCommand()
            {
                Name=_CreateProductDto.Name,
                ManufactureEmail=_CreateProductDto.ManufactureEmail,
                ManufacturePhone=_CreateProductDto.ManufacturePhone,
                ProduceDate = _CreateProductDto.ProduceDate,
                UserId = _CreateProductDto.UserId
            }, CancellationToken.None);

            result.ShouldBeOfType<int>();

            var products = await _mockRepository.Object.ProductRepository.GetAllAsync();

            products.Count().ShouldBe(3);
        }

     
    }
}
