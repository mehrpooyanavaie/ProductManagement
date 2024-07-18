using MediatR;
using ProductApi.Domain.Entities;
using ProductApi.Domain.VM;

namespace ProductApi.Application.Products.Queries
{
    public class GetProductByIdQuery : IRequest<ProductVM>
    {
        public int Id { get; set; }
    }
}
