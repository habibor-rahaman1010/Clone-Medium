using MediatR;
using Medium.Domain.Entities;
using Medium.Domain.UnitOfWorkInterface;

namespace Medium.Application.Features.Categories.Queries
{
    public class GetAllCategorisQueryHandler : IRequestHandler<GetAllCategorisQuery, List<Category>>
    {
        private readonly IMediumUnitOfWork _mediumUnitOfWork;

        public GetAllCategorisQueryHandler(IMediumUnitOfWork mediumUnitOfWork)
        {
            _mediumUnitOfWork = mediumUnitOfWork;
        }

        public async Task<List<Category>> Handle(GetAllCategorisQuery request, CancellationToken cancellationToken)
        {
            var categories = await _mediumUnitOfWork.CategoryRepository.GetAllAsync(cancellationToken);
            return [..categories];
        }
    }
}
