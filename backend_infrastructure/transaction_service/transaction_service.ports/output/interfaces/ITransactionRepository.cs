using transaction_service.domain.entities;

namespace transaction_service
{
    public partial interface ITransactionRepository
    {
        Task AddAsync(TransactionEntity entity, CancellationToken cancellationToken);
        Task<IEnumerable<TransactionEntity>> GetFilteredAsync(DateTime? start, DateTime? end, string? type, CancellationToken cancellationToken);
    }
}
