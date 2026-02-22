using transaction_service.ports.dtos.request;
using transaction_service.ports.dtos.response;

namespace transaction_service
{
    public partial class TransactionUseCase : ITransactionUseCase
    {
        public async Task<IEnumerable<TransactionResponse>> ExecuteAsync(TransactionFilterRequest filter, CancellationToken cancellationToken)
        {
            var data = await _transactionRepository.GetFilteredAsync(filter.StartDate, filter.EndDate, filter.Type, cancellationToken);
            return data
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(transaction => new TransactionResponse
                {
                    Id = transaction.Id,
                    Date = transaction.Date,
                    TransactionType = transaction.TransactionType,
                    ProductId = transaction.ProductId,
                    ProductName = transaction.ProductName,
                    Quantity = transaction.Quantity,
                    UnitPrice = transaction.UnitPrice,
                    TotalPrice = transaction.TotalPrice,
                    Detail = transaction.Detail
                });
        }
    }
}
