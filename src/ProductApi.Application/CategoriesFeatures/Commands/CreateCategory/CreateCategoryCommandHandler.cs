using AutoMapper;
using MediatR;
using ProductApi.Application.Interfaces;
using ProductApi.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;

namespace ProductApi.Application.CategoriesFeatures.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Category>(request);
            int myCategoryId = await _unitOfWork.CategoryRepository.AddAsync(category);
            return myCategoryId;
        }
    }
}