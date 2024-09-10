using AutoMapper;
using MediatR;
using ProductApi.Application.Interfaces;
using ProductApi.Domain.Entities;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using StackExchange.Redis;
using System.Text.Json;
using ProductApi.Application.DTOs;

namespace ProductApi.Application.ProductsFeatures.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Unit>
    {
        private readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork;
        private readonly IDatabase _db;
        private readonly string _cacheKey = "ProductList";

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IConnectionMultiplexer redis)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _db = redis.GetDatabase();
        }

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {

            var product = await _unitOfWork.ProductRepository.HelpToUpdateAsync(request.Id);

            if (product == null)
            {
                return Unit.Value;
            }
            _mapper.Map(request, product);
            product.categoryProducts.Clear();
            foreach (var categoryId in request.CategoryIds)
            {
                var category = await _unitOfWork.CategoryRepository.GetByIdAsync(categoryId);
                if (category != null)
                {
                    product.categoryProducts.Add(new CategoryProduct { ProductId = product.Id, CategoryId = categoryId });
                }
            }
            await _unitOfWork.ProductRepository.UpdateAsync(product);
            await _unitOfWork.SaveAsync();
            var productsSerialized = await _db.ListRangeAsync(_cacheKey);
            if (productsSerialized.Count() > 0)
            {
                foreach (var item in productsSerialized)
                {

                    var myP = JsonSerializer.Deserialize<RedisProductDTO>(item);

                    if (myP.Id == request.Id)
                    {
                        RedisProductDTO redisProductDTO = _mapper.Map<RedisProductDTO>(request);
                        redisProductDTO.ManufactureEmail = myP.ManufactureEmail;
                        redisProductDTO.UserId = myP.UserId;
                        await _db.ListRemoveAsync(_cacheKey, item);
                        string serializedProduct = JsonSerializer.Serialize(redisProductDTO);
                        await _db.ListRightPushAsync(_cacheKey, serializedProduct);
                        break;
                    }
                }
            }
            return Unit.Value;
        }
    }
}