using transaction_service.domain.entities;

namespace transaction_service
{
    public partial interface ITransactionRepository
    {
        Task<IEnumerable<TransactionEntity>> GetFilteredTransactionAsync(DateTime? start, DateTime? end, string? type, CancellationToken cancellationToken);
        Task AddTransactionAsync(TransactionEntity entity, CancellationToken cancellationToken);
        Task<TransactionEntity?> GetTransactionByIdAsync(string id, CancellationToken cancellationToken);
    }
}
