using Microsoft.EntityFrameworkCore;
using transaction_service.domain.entities;

namespace transaction_service
{
    public partial class TransactionRepository: ITransactionRepository
    {
        public async Task<IEnumerable<TransactionEntity>> GetFilteredTransactionAsync(DateTime? start, DateTime? end, string? type, CancellationToken cancellationToken)
        {
            var query = _context.Transactions.AsNoTracking().AsQueryable();
            if (start.HasValue) query = query.Where(transaction => transaction.Date >= start.Value);
            if (end.HasValue) query = query.Where(transaction => transaction.Date <= end.Value);
            if (!string.IsNullOrEmpty(type)) query = query.Where(transaction => transaction.TransactionType == type);
            return await query.OrderByDescending(transaction => transaction.Date).ToListAsync(cancellationToken);
        }
        public async Task<TransactionEntity?> GetTransactionByIdAsync(string id, CancellationToken cancellationToken)
        {
            return await _context.Transactions.FirstOrDefaultAsync(transaction => transaction.Id == id, cancellationToken);
        }
        public async Task AddTransactionAsync(TransactionEntity entity, CancellationToken cancellationToken)
        {
            await _context.Transactions.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }        
    }
}