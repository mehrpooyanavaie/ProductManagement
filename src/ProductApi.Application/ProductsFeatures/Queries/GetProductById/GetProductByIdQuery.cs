using MediatR;
using ProductApi.Domain.Entities;
using ProductApi.Application.VM;

namespace ProductApi.Application.ProductsFeatures.Queries.GetProductById
{
    public class GetProductByIdQuery : IRequest<ProductVM>
    {
        public int Id { get; set; }
    }
}
