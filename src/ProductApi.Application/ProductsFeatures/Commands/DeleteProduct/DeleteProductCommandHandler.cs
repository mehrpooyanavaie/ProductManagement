using MediatR;
using ProductApi.Application.Interfaces;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using ProductApi.Domain.Entities;
using ProductApi.Application.DTOs;

namespace ProductApi.Application.ProductsFeatures.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
    {
        private IUnitOfWork _unitOfWork;
        private readonly IDatabase _db;
        private readonly string _cacheKey = "ProductList";
        public DeleteProductCommandHandler(IUnitOfWork unitOfWork, IConnectionMultiplexer redis)
        {
            _unitOfWork = unitOfWork;
            _db = redis.GetDatabase();
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var productsSerialized = await _db.ListRangeAsync(_cacheKey);
            if (productsSerialized.Count() > 0)
            {
                foreach (var item in productsSerialized)
                {
                    var product = JsonSerializer.Deserialize<RedisProductDTO>(item);

                    if (product.Id == request.Id)
                    {
                        await _db.ListRemoveAsync(_cacheKey, item);
                        break;
                    }
                }
            }

            await _unitOfWork.ProductRepository.DeleteAsync(request.Id);
            await _unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }
}