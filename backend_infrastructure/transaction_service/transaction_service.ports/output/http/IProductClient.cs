namespace transaction_service
{
    namespace transaction_service.ports.output.http
    {
        public interface IProductClient
        {
            Task<(bool Success, string ProductName, string Message)> UpdateProductStockAsync(
                string productId,
                int quantity,
                string transactionType,
                CancellationToken cancellationToken);
        }
    }
}
