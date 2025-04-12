using MediatR;
using Medium.Domain.UnitOfWorkInterface;

namespace Medium.Application.Features.Categories.Commands
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool>
    {
        private readonly IMediumUnitOfWork _mediumUnitOfWork;

        public DeleteCategoryCommandHandler(IMediumUnitOfWork mediumUnitOfWork)
        {
            _mediumUnitOfWork = mediumUnitOfWork;
        }

        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var deleteToCategory = await _mediumUnitOfWork.CategoryRepository.GetByIdAsync(request.Id);

            if (deleteToCategory == null)
            {
                return await Task.FromResult(false); ;
            }

            await _mediumUnitOfWork.CategoryRepository.DeleteAsync(deleteToCategory, cancellationToken);
            await _mediumUnitOfWork.SaveAsync();
            return await Task.FromResult(true);
        }
    }
}
