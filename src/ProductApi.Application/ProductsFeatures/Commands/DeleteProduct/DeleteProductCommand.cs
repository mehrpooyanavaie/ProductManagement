using MediatR;
namespace ProductApi.Application.ProductsFeatures.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}