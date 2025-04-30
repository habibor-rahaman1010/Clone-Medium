using MediatR;
using Medium.Domain.Entities;

namespace Medium.Application.Features.Categories.Queries
{
    public class GetAllCategorisQuery : IRequest<(IList<Category> items, int currentPage, int totalPages, int totalItems, int pageSize)>
    {
        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }


        public GetAllCategorisQuery(int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
    }
}
