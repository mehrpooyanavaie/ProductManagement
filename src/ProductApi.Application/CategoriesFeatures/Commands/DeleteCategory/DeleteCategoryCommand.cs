using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProductApi.Application.CategoriesFeatures.Commands.DeleteCategory
{
    public class DeleteCategoryCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}