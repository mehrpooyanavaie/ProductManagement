using MediatR;
using ProductApi.Application.Interfaces;
using ProductApi.Domain.Entities;
using ProductApi.Application.VM;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;


namespace ProductApi.Application.ProductsFeatures.Queries.GetProductsByUserId
{
    public class GetProductsByUserIdQueryHandler : IRequestHandler<GetProductsByUserIdQuery, IEnumerable<ProductVM>>
    {
        private readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork;
        public GetProductsByUserIdQueryHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ProductVM>> Handle(GetProductsByUserIdQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<ProductVM> myProductsVM = _mapper.Map<IEnumerable<ProductVM>>
                (await _unitOfWork.ProductRepository.GetProductsByUserIdAsync(request.UserId));
            return myProductsVM;
        }
    }
}