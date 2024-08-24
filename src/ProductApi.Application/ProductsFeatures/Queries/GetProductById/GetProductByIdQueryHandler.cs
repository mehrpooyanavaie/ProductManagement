using MediatR;
using ProductApi.Application.Interfaces;
using ProductApi.Domain.Entities;
using ProductApi.Application.VM;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;

namespace ProductApi.Application.ProductsFeatures.Queries.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductVM>
    {
        private readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public GetProductByIdQueryHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductVM> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var myProductVM = _mapper.Map<ProductVM>(await _unitOfWork.ProductRepository.GetByIdAsync(request.Id));
            return myProductVM;
        }
    }
}