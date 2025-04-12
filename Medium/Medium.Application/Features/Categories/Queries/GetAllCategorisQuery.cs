using MediatR;
using Medium.Domain.Entities;

namespace Medium.Application.Features.Categories.Queries
{
    public class GetAllCategorisQuery : IRequest<List<Category>>
    {
    }
}
