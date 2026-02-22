using transaction_service.ports.dtos.request;
using transaction_service.ports.dtos.response;

namespace transaction_service
{
    public partial interface ITransactionUseCase
    {
        Task<Guid> ExecuteAsync(TransactionRequest request, CancellationToken cancellationToken);
        Task<IEnumerable<TransactionResponse>> ExecuteAsync(TransactionFilterRequest filter, CancellationToken cancellationToken);
    }
}
