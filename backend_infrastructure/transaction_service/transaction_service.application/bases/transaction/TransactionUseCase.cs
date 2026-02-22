namespace transaction_service
{
    public partial class TransactionUseCase : ITransactionUseCase
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly HttpClient _httpClient;
        private readonly IProductClient _productClient;

        public TransactionUseCase(
            ITransactionRepository transactionRepository,
            HttpClient httpClient,
            IProductClient productClient
            )
        {
            _transactionRepository = transactionRepository;
            _httpClient = httpClient;
            _productClient = productClient;
        }
    }
}
