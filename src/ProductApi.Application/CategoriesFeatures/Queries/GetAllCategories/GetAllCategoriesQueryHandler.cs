using MediatR;
using ProductApi.Domain.Entities;
using ProductApi.Application.VM;
using System.Collections.Generic;
using ProductApi.Application.Interfaces;
using AutoMapper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
namespace ProductApi.Application.CategoriesFeatures.Queries.GetAllCategories
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryVM>>
    {

        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllCategoriesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CategoryVM>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();
            var myCategoryVM = _mapper.Map<List<CategoryVM>>(categories);
            return myCategoryVM;
        }
    }
}