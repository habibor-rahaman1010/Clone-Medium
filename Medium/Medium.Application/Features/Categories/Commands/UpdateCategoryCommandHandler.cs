using MediatR;
using Medium.Domain;
using Medium.Domain.UnitOfWorkInterface;

namespace Medium.Application.Features.Categories.Commands
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, bool>
    {
        private readonly IMediumUnitOfWork _mediumUnitOfWork;
        private readonly IApplicationTime _applicationTime;

        public UpdateCategoryCommandHandler(IMediumUnitOfWork mediumUnitOfWork, IApplicationTime applicationTime)
        {
            _mediumUnitOfWork = mediumUnitOfWork;
            _applicationTime = applicationTime;
        }

        public async Task<bool> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryToUpdate = await _mediumUnitOfWork.CategoryRepository.GetByIdAsync(request.Id, cancellationToken);
            if(categoryToUpdate == null)
            {
                return false;
            }
            categoryToUpdate.Name = request.Name;
            categoryToUpdate.Description = request.Description;
            categoryToUpdate.UpdatedDate = _applicationTime.GetCurrentDateTime();
            await _mediumUnitOfWork.CategoryRepository.UpdateAsync(categoryToUpdate, cancellationToken);
            await _mediumUnitOfWork.SaveAsync();

            return true;
        }
    }
}
