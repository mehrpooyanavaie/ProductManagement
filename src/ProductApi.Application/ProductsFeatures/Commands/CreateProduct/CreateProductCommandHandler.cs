using AutoMapper;
using MediatR;
using ProductApi.Application.Interfaces;
using ProductApi.Domain.Entities;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using ProductApi.Application.DTOs;

namespace ProductApi.Application.ProductsFeatures.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDatabase _db;
        private readonly string _cacheKey = "ProductList";

        public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IConnectionMultiplexer redis)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _db = redis.GetDatabase();
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var myProduct = _mapper.Map<Product>(request);
            myProduct.UserId = request.UserId;
            foreach (var categoryId in request.CategoryIds)
            {
                var myCategory = await _unitOfWork.CategoryRepository.GetByIdAsync(categoryId);
                if (myCategory != null)
                {
                    myProduct.categoryProducts.Add(new CategoryProduct { product = myProduct, category = myCategory });
                }
            }
            int MyProductId = await _unitOfWork.ProductRepository.AddAsync(myProduct);
            var listLength = await _db.ListLengthAsync(_cacheKey);
            if (listLength > 0)
            {
                var redisProductDTO = _mapper.Map<RedisProductDTO>(request);
                redisProductDTO.Id = MyProductId;
                string serializedProduct = JsonSerializer.Serialize(redisProductDTO);
                await _db.ListRightPushAsync(_cacheKey, serializedProduct);
            }
            return MyProductId;
        }
    }
}