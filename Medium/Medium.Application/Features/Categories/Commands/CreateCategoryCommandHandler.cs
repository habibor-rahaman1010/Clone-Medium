using MediatR;
using Medium.Domain;
using Medium.Domain.Entities;
using Medium.Domain.UnitOfWorkInterface;

namespace Medium.Application.Features.Categories.Commands
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, bool>
    {
        private readonly IMediumUnitOfWork _mediumUnitOfWork;
        private readonly IApplicationTime _applicationTime;

        public CreateCategoryCommandHandler(IMediumUnitOfWork mediumUnitOfWork, IApplicationTime applicationTime)
        {
            _mediumUnitOfWork = mediumUnitOfWork;
            _applicationTime = applicationTime;
        }

        public async Task<bool> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                CreatedDate = _applicationTime.GetCurrentDateTime(),
                UpdatedDate = _applicationTime.GetCurrentDateTime(),
            };

            await _mediumUnitOfWork.CategoryRepository.AddAsync(category);
            await _mediumUnitOfWork.SaveAsync();
            return true;
        }
    }
}
