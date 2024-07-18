
using MediatR;
using ProductApi.Domain.Entities;
using ProductApi.Domain.VM;
using System.Collections.Generic;

namespace ProductApi.Application.Products.Queries
{
    public class GetProductsByUserIdQuery : IRequest<IEnumerable<ProductVM>>
    {
        public string UserId { get; set; }
    }
}