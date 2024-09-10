using MediatR;
using ProductApi.Domain.Entities;
using ProductApi.Application.VM;
using System.Collections.Generic;
using ProductApi.Application.Interfaces;
using AutoMapper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
namespace ProductApi.Application.CategoriesFeatures.Queries.GetCategoryById
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryVM>
    {

        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetCategoryByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CategoryVM> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(request.Id);
            var categoryVM = _mapper.Map<CategoryVM>(category);
            return categoryVM;
        }
    }
}