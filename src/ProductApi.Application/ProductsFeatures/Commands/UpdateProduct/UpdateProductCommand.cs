using MediatR;
using System;
using System.Text.Json.Serialization;

namespace ProductApi.Application.ProductsFeatures.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ProduceDate { get; set; }
        public string ManufacturePhone { get; set; }
        public bool IsAvailable { get; set; }
    }
}
