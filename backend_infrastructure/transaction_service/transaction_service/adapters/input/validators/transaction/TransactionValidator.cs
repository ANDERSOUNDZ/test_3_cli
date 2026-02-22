using FluentValidation;
using transaction_service.ports.dtos.request;

namespace transaction_service.adapters.input.validators.transaction
{
    public class TransactionValidator : AbstractValidator<TransactionRequest>
    {
        public TransactionValidator()
        {
            RuleFor(x => x.TransactionType)
                .NotEmpty().WithMessage("El tipo de transacción es requerido.")
                .MaximumLength(50);
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("El ID del producto es obligatorio.");
            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("La cantidad debe ser mayor a cero.");
            RuleFor(x => x.UnitPrice)
                .GreaterThan(0).WithMessage("El precio unitario debe ser mayor a cero.");
            RuleFor(x => x.Detail)
                .MaximumLength(500).WithMessage("El detalle no puede exceder los 500 caracteres.");
        }
    }
}
