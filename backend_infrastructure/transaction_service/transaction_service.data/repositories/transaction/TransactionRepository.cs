using Microsoft.EntityFrameworkCore;
using transaction_service.domain.entities;

namespace transaction_service
{
    public partial class TransactionRepository: ITransactionRepository
    {
        public async Task<IEnumerable<TransactionEntity>> GetFilteredAsync(DateTime? start, DateTime? end, string? type, CancellationToken cancellationToken)
        {
            var query = _context.Transactions.AsNoTracking().AsQueryable();
            if (start.HasValue) query = query.Where(t => t.Date >= start.Value);
            if (end.HasValue) query = query.Where(t => t.Date <= end.Value);
            if (!string.IsNullOrEmpty(type)) query = query.Where(t => t.TransactionType == type);
            return await query.OrderByDescending(t => t.Date).ToListAsync(cancellationToken);
        }
        public async Task AddAsync(TransactionEntity entity, CancellationToken cancellationToken)
        {
            await _context.Transactions.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }        
    }
}