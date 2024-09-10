
using MediatR;
using ProductApi.Domain.Entities;
using ProductApi.Application.VM;
using System.Collections.Generic;
using StackExchange.Redis;
using ProductApi.Application.Interfaces;
using AutoMapper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using ProductApi.Application.DTOs;
namespace ProductApi.Application.ProductsFeatures.Queries.GetProductsByCategoryId
{
    public class GetProductsByCategoryIdQueryHandler : IRequestHandler<GetProductsByCategoryIdQuery, IEnumerable<ProductVM>>
    {

        private readonly IDatabase _db;
        private readonly string _cacheKey = "ProductList";
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetProductsByCategoryIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IConnectionMultiplexer redis)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _db = redis.GetDatabase();
        }

        public async Task<IEnumerable<ProductVM>> Handle(GetProductsByCategoryIdQuery request, CancellationToken cancellationToken)
        {
            var productsSerialized = await _db.ListRangeAsync(_cacheKey);
            var productsDTO = new List<RedisProductDTO>();

            if (productsSerialized.Any())
            {
                foreach (var item in productsSerialized)
                {
                    var productDTO = JsonSerializer.Deserialize<RedisProductDTO>(item);
                    foreach (var catId in productDTO.CategoryIds)
                    {
                        if (request.CategoryId == catId)
                        {
                            productsDTO.Add(productDTO);
                            break;
                        }
                    }
                }

            }
            if (productsDTO.Any())
            {
                var products = _mapper.Map<IEnumerable<Product>>(productsDTO);

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

                var productsVM = _mapper.Map<IEnumerable<ProductVM>>(products);

                var totalcount = productsVM.Count();
                var totalpages = (int)Math.Ceiling((decimal)totalcount / request.PageSize);
                var productsPerPage = productsVM.Skip((request.Page - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToList();

                return productsPerPage;
            }
            else
            {
                var products = await _unitOfWork.ProductRepository.GetProductsByCategoryIdAsync(request.CategoryId);

                var productVM = _mapper.Map<List<ProductVM>>(products);
                var totalcount = productVM.Count();
                var totalpages = (int)Math.Ceiling((decimal)totalcount / request.PageSize);
                var productsPerPage = productVM.Skip((request.Page - 1) * request.PageSize)
                    .Take(request.PageSize)
                        .ToList();
                return productsPerPage;
            }

        }
    }
}
