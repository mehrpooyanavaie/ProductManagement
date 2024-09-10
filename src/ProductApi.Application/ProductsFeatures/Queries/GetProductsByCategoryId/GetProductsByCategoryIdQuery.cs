using MediatR;
using ProductApi.Application.VM;
namespace ProductApi.Application.ProductsFeatures.Queries.GetProductsByCategoryId
{
    public class GetProductsByCategoryIdQuery : IRequest<IEnumerable<ProductVM>>
    {
        public int CategoryId { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}