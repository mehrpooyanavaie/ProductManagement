using MediatR;
using ProductApi.Domain.Entities;
using ProductApi.Application.VM;
using System.Collections.Generic;

namespace ProductApi.Application.CategoriesFeatures.Queries.GetAllCategories
{
    public class GetAllCategoriesQuery : IRequest<IEnumerable<CategoryVM>>
    {
        
    }
}