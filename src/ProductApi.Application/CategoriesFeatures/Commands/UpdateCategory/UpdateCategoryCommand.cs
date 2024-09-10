using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProductApi.Application.CategoriesFeatures.Commands.UpdateCategory
{
    public class UpdateCategoryCommand : IRequest<Unit>
    {
        [JsonIgnore]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}