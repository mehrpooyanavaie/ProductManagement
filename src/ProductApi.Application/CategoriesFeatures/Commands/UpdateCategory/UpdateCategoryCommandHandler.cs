using AutoMapper;
using MediatR;
using ProductApi.Application.Interfaces;
using ProductApi.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;

namespace ProductApi.Application.CategoriesFeatures.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Unit>
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Category>(request);
            await _unitOfWork.CategoryRepository.UpdateAsync(category);
            return Unit.Value;
        }
    }
}