using MediatR;
using ProductApi.Application.Interfaces;
using ProductApi.Domain.Entities;
using ProductApi.Application.VM;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using StackExchange.Redis;
using ProductApi.Application.DTOs;

namespace ProductApi.Application.ProductsFeatures.Queries.GetProducts
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<ProductVM>>
    {
        private readonly IDatabase _db;
        private readonly string _cacheKey = "ProductList";
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetProductsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IConnectionMultiplexer redis)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _db = redis.GetDatabase();
        }

        public async Task<IEnumerable<ProductVM>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            int startIndex = (request.Page - 1) * request.PageSize;
            int endIndex = request.Page * request.PageSize - 1;

            var productsSerialized = await _db.ListRangeAsync(_cacheKey, startIndex, endIndex);
            var deserializedProductList = productsSerialized
                .Select(item => JsonSerializer.Deserialize<RedisProductDTO>(item)).ToList();

            if (deserializedProductList.Any())
            {
                var products = _mapper.Map<List<Product>>(deserializedProductList);
                foreach (var product in products)
                {
                    var categoryIds = product.categoryProducts.Select(cp => cp.CategoryId).ToList();
                    var categories = await _unitOfWork.CategoryRepository.GetCategoriesByIdsAsync(categoryIds);
                    foreach (var categoryProduct in product.categoryProducts)
                    {
                        var category = categories.FirstOrDefault(c => c.Id == categoryProduct.CategoryId);
                        if (category != null)
                        {
                            categoryProduct.category = new Category
                            {
                                Id = category.Id,
                                Name = category.Name
                            };
                        }
                    }
                }
                return _mapper.Map<List<ProductVM>>(products);
            }
            else
            {

                var myProducts = await _unitOfWork.ProductRepository.GetAllAsync();

                var myRedisProduct = _mapper.Map<List<RedisProductDTO>>(myProducts);
                foreach (var product in myRedisProduct)
                {
                    string serializedProduct = JsonSerializer.Serialize(product);
                    await _db.ListRightPushAsync(_cacheKey, serializedProduct);
                }

                var expiration = TimeSpan.FromMinutes(30);
                await _db.KeyExpireAsync(_cacheKey, expiration);

                var productsPerPage = myProducts.Skip((request.Page - 1) * request.PageSize)
                    .Take(request.PageSize).ToList();

                return _mapper.Map<List<ProductVM>>(productsPerPage);
            }
        }

    }
}
