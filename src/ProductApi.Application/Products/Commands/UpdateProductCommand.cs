using MediatR;
using System;

namespace ProductApi.Application.Products.Commands
{
    public class UpdateProductCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ProduceDate { get; set; }
        public string ManufacturePhone { get; set; }
        public string ManufactureEmail { get; set; }

        public bool IsAvailable { get; set; }
    }
}
