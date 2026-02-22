using FluentValidation;
using product_service.ports.dtos.request.product;

namespace product_service.adapters.input.validators.product
{
    public class CreateProductValidator : AbstractValidator<ProductRequest>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre del producto es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres.");
            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("El precio debe ser un valor mayor a cero.");
            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(0).WithMessage("El stock no puede ser un número negativo.");
            RuleFor(x => x.CategoryId).NotNull().WithMessage("La categoría es obligatoria.");
            RuleFor(x => x.Image)
                .MaximumLength(500).WithMessage("La URL de la imagen es demasiado larga.");
        }
    }
}