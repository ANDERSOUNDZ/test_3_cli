using transaction_service.domain.entities;
using transaction_service.ports.dtos.request;

namespace transaction_service
{
    public partial class TransactionUseCase : ITransactionUseCase
    {
        public async Task<Guid> ExecuteAsync(TransactionRequest request, CancellationToken cancellationToken)
        {
            var entity = new TransactionEntity
            {
                Id = Guid.NewGuid(),
                Date = DateTime.UtcNow,
                TransactionType = request.TransactionType,
                ProductId = request.ProductId,
                Quantity = request.Quantity,
                UnitPrice = request.UnitPrice,
                TotalPrice = request.Quantity * request.UnitPrice,
                Detail = request.Detail
            };
            await _transactionRepository.AddAsync(entity, cancellationToken);
            return entity.Id;
        }
    }
}
