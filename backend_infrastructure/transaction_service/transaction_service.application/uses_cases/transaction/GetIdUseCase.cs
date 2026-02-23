using transaction_service.ports.dtos.request;
using transaction_service.ports.dtos.response;

namespace transaction_service
{
    public partial class TransactionUseCase : ITransactionUseCase
    {
        public async Task<TransactionResponse?> ExecuteAsync(GetTransactionRequest request, CancellationToken cancellationToken)
        {
            var transaction = await _transactionRepository.GetTransactionByIdAsync(request.Id, cancellationToken);
            if (transaction == null) return null;

            return new TransactionResponse
            {
                Id = transaction.Id,
                ProductId = transaction.ProductId,
                Date = transaction.Date,
                TransactionType = transaction.TransactionType,
                ProductName = transaction.ProductName,
                Quantity = transaction.Quantity,
                UnitPrice = transaction.UnitPrice,
                TotalPrice = transaction.TotalPrice,
                Detail = transaction.Detail
            };
        }
    }
}
