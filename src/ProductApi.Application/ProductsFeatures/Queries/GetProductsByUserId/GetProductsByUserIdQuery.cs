
using MediatR;
using ProductApi.Domain.Entities;
using ProductApi.Application.VM;
using System.Collections.Generic;

namespace ProductApi.Application.ProductsFeatures.Queries.GetProductsByUserId
{
    public class GetProductsByUserIdQuery : IRequest<IEnumerable<ProductVM>>
    {
        public string UserId { get; set; }
    }
}