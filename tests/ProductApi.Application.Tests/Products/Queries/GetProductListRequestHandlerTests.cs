using AutoMapper;
using ProductApi.Application.Interfaces;
using ProductApi.Domain.Entities;
using ProductApi.Application.ProductsFeatures.Queries.GetProducts;  
using ProductApi.Application.Mappings;
using ProductApi.Application.Tests.Mocks;
using Moq;
using Shouldly;
using Xunit;
using ProductApi.Application.VM;
//ddddd
namespace ProductApi.Application.Tests.Products.Queries
{
    public class GetProductListRequestHandlerTests
    {
        IMapper _mapper;
        Mock<IUnitOfWork> _mockRepository;
        public GetProductListRequestHandlerTests()
        {
            _mockRepository = MockProductRepository.GetProductRepository();

            var mapperConfig = new MapperConfiguration(m =>
            {
                m.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetLeaveTypeListTest()
        {
            var handler = new GetProductsQueryHandler(_mockRepository.Object, _mapper);
            var result = await handler.Handle(new GetProductsQuery(), CancellationToken.None);

            result.ShouldBeOfType<List<ProductVM>>();
            result.Count().ShouldBe(2);
        }
    }
}
