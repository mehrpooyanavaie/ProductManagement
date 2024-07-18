using MediatR;
using ProductApi.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace ProductApi.Application.Products.Commands
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
    {
        private IUnitOfWork _unitOfWork;
        public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.ProductRepository.DeleteAsync(request.Id);
            await _unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }
}