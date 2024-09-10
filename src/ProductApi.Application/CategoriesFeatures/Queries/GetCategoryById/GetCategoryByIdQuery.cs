using MediatR;
using ProductApi.Domain.Entities;
using ProductApi.Application.VM;
using System.Collections.Generic;

namespace ProductApi.Application.CategoriesFeatures.Queries.GetCategoryById
{
    public class GetCategoryByIdQuery : IRequest<CategoryVM>
    {
        public int Id { get; set; }
    }
}