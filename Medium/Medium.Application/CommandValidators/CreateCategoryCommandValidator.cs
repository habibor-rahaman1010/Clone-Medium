using FluentValidation;
using Medium.Application.Features.Categories.Commands;


namespace Medium.Application.CommandValidators
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MinimumLength(5).WithMessage("Name must be at least 5 characters.")
            .MaximumLength(50).WithMessage("Name cannot exceed 50 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(100).WithMessage("Description is too long.");
        }
    }
}
