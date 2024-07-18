using MediatR;
using ProductApi.Domain.Entities;
using System.Collections.Generic;
using ProductApi.Domain.VM;

namespace ProductApi.Application.Products.Queries
{
    public class GetProductsQuery : IRequest<IEnumerable<ProductVM>>
    {
    }
}