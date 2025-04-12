using MediatR;
using Medium.Domain.Entities;
using Medium.Domain.UnitOfWorkInterface;

namespace Medium.Application.Features.Categories.Queries
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Category>
    {
        private readonly IMediumUnitOfWork _mediumUnitOfWork;

        public GetCategoryByIdQueryHandler(IMediumUnitOfWork mediumUnitOfWork)
        {
            _mediumUnitOfWork = mediumUnitOfWork;
        }

        public async Task<Category> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _mediumUnitOfWork.CategoryRepository.GetByIdAsync(request.Id);
            if (category == null)
            {
                return null;
            }

            return category;
        }
    }
}
