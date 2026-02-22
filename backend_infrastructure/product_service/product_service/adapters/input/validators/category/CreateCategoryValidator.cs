using FluentValidation;
using product_service.ports.dtos.request.category;

namespace product_service.adapters.input.validators.category
{
    public class CreateCategoryValidator : AbstractValidator<CategoryRequest>
    {
        public CreateCategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre de la categoría es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre de la categoría no puede exceder los 100 caracteres.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("La descripción no puede exceder los 500 caracteres.");
        }
    }
}
