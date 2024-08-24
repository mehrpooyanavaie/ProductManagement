using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProductApi.Application.ProductsFeatures.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<int>
    {
        [Required]
        public string Name { get; set; }
        public DateTime ProduceDate { get; set; }
        public string ManufacturePhone { get; set; }
        [JsonIgnore]
        public string? ManufactureEmail { get; set; }
         [Required]
        public bool IsAvailable { get; set; }
        [JsonIgnore]
        public string? UserId { get; set; }
    }
}