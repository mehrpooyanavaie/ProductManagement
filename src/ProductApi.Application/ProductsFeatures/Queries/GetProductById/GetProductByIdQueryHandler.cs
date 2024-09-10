using MediatR;
using ProductApi.Application.Interfaces;
using ProductApi.Domain.Entities;
using ProductApi.Application.VM;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using StackExchange.Redis;
using System.Text.Json;
using ProductApi.Application.DTOs;

namespace ProductApi.Application.ProductsFeatures.Queries.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductVM>
    {
        private readonly IDatabase _db;
        private readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork;
        private readonly string _cacheKey = "ProductList";

        public GetProductByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IConnectionMultiplexer redis)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _db = redis.GetDatabase();
        }

        public async Task<ProductVM> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var productsSerialized = await _db.ListRangeAsync(_cacheKey);
            if (productsSerialized.Count() > 0)
            {
                foreach (var item in productsSerialized)
                {

                    var productDTO = JsonSerializer.Deserialize<RedisProductDTO>(item);


                    if (productDTO.Id == request.Id)
                    {
                        List<Category> categories = await _unitOfWork.CategoryRepository.
                            GetCategoriesByIdsAsync(productDTO.CategoryIds);
                        List<string> categoriesName = new List<string>();
                        foreach (var category in categories)
                        {
                            categoriesName.Add(category.Name);
                        }
                        ProductVM productVM = _mapper.Map<ProductVM>(productDTO);
                        productVM.CategoryNames = categoriesName;

                        return productVM;
                    }
                }
            }

            var myProductVM = _mapper.Map<ProductVM>
                (await _unitOfWork.ProductRepository.GetByIdAsync(request.Id));
            return myProductVM;
        }
    }
}