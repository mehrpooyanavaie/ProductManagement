using MediatR;
using System;
using System.Text.Json.Serialization;

namespace ProductApi.Application.ProductsFeatures.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<Unit>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ProduceDate { get; set; }
        [System.ComponentModel.DataAnnotations.Phone]
        public string ManufacturePhone { get; set; }
        public bool IsAvailable { get; set; }
        public List<int> CategoryIds { get; set; }
    }
}
