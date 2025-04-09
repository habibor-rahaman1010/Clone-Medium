using MediatR;

namespace Medium.Application.Features.Categories.Commands
{
    public class CreateCategoryCommand : IRequest<bool>
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
