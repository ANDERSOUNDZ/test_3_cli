using transaction_service.domain.entities;
using transaction_service.ports.dtos.request;

namespace transaction_service
{
    public partial class TransactionUseCase : ITransactionUseCase
    {
        public async Task<string> ExecuteAsync(TransactionRequest request, CancellationToken cancellationToken)
        {
            var entity = new TransactionEntity
            {
                Id = Guid.NewGuid().ToString("N"),
                Date = DateTime.UtcNow,
                TransactionType = request.TransactionType,
                ProductId = request.ProductId,
                Quantity = request.Quantity,
                UnitPrice = request.UnitPrice,
                TotalPrice = request.Quantity * request.UnitPrice,
                Detail = request.Detail,
                ProductName = "Pendiente..."
            };

            await _transactionRepository.AddAsync(entity, cancellationToken);
            return entity.Id;
        }
    }
}