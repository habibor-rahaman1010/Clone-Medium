using MediatR;
using Medium.Domain.Entities;

namespace Medium.Application.Features.Categories.Queries
{
    public class GetCategoryByIdQuery : IRequest<Category>
    {
        public Guid Id { get; set; }
    }
}
