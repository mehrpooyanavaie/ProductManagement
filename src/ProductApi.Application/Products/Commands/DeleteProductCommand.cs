using MediatR;
namespace ProductApi.Application.Products.Commands
{
    public class DeleteProductCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}