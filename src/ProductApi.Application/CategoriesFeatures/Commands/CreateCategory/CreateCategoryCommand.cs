using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProductApi.Application.CategoriesFeatures.Commands.CreateCategory
{
    public class CreateCategoryCommand : IRequest<int>
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}