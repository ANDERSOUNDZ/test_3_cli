using transaction_service.data.context;

namespace transaction_service
{
    public partial class TransactionRepository : ITransactionRepository
    {
        private readonly TransactionDbContext _context;
        public TransactionRepository(TransactionDbContext context)
        {
            _context = context;
        }
    }
}
