using MediatR;
using Medium.Domain.Entities;
using Medium.Domain.UnitOfWorkInterface;

namespace Medium.Application.Features.Categories.Queries
{
    public class GetAllCategorisQueryHandler : IRequestHandler<GetAllCategorisQuery, (IList<Category> items, int currentPage, int totalPages, int totalItems, int pageSize)>
    {
        private readonly IMediumUnitOfWork _mediumUnitOfWork;

        public GetAllCategorisQueryHandler(IMediumUnitOfWork mediumUnitOfWork)
        {
            _mediumUnitOfWork = mediumUnitOfWork;
        }

        public async Task<(IList<Category> items, int currentPage, int totalPages, int totalItems, int pageSize)> Handle(GetAllCategorisQuery request, CancellationToken cancellationToken)
        {
            var categories = await _mediumUnitOfWork.CategoryRepository.GetAllAsync(request.PageIndex, request.PageSize, cancellationToken);
            return categories;
        }
    }
}
