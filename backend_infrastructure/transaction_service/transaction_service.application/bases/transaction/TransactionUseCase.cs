namespace transaction_service
{
    public partial class TransactionUseCase : ITransactionUseCase
    {
        private readonly ITransactionRepository _transactionRepository;
        public TransactionUseCase(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
    }
}
