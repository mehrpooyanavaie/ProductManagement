using MediatR;
using ProductApi.Application.Interfaces;
using ProductApi.Domain.Entities;
using ProductApi.Domain.VM;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;

namespace ProductApi.Application.Products.Queries
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<ProductVM>>
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetProductsQueryHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ProductVM>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var myProducts = await _unitOfWork.ProductRepository.GetAllAsync();
            IEnumerable<ProductVM> myProductsVM = _mapper.Map<IEnumerable<ProductVM>>(myProducts);
            return myProductsVM;
        }
    }
}