using AutoMapper;
using MediatR;
using ProductApi.Application.Interfaces;
using ProductApi.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using StackExchange.Redis;
using ProductApi.Application.DTOs;

namespace ProductApi.Application.CategoriesFeatures.Commands.DeleteCategory
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Unit>
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDatabase _db;
        private readonly string _cacheKey = "ProductList";

        public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IConnectionMultiplexer redis)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _db = redis.GetDatabase();
        }
        public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(request.Id);
            if (category == null)
            {
                throw new Exception("Category not found");
            }

            var redisProductList = await _db.ListRangeAsync(_cacheKey);
            if (redisProductList.Count() > 0)
            {
                foreach (var item in redisProductList)
                {
                    var redisProduct = JsonSerializer.Deserialize<RedisProductDTO>(item);
                    if (redisProduct.CategoryIds.Contains(request.Id) == true)
                    {

                        redisProduct.CategoryIds.Remove(request.Id);
                        await _db.ListRemoveAsync(_cacheKey, item);
                        if (redisProduct.CategoryIds.Any() == true)
                        {
                            string serializedProduct = JsonSerializer.Serialize(redisProduct);
                            await _db.ListRightPushAsync(_cacheKey, serializedProduct);
                        }
                    }
                }
            }

            await _unitOfWork.CategoryRepository.HelpToDeleteCategoryAsync(request.Id);

            await _unitOfWork.CategoryRepository.DeleteAsync(request.Id);

            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
