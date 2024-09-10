using MediatR;
using ProductApi.Domain.Entities;
using System.Collections.Generic;
using ProductApi.Application.VM;

namespace ProductApi.Application.ProductsFeatures.Queries.GetProducts
{
    public class GetProductsQuery : IRequest<IEnumerable<ProductVM>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}