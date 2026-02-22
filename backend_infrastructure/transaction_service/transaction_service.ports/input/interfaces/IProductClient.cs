namespace transaction_service
{
    public partial interface IProductClient
    {
        Task<(bool Success, string ProductName, string Message)> UpdateProductStockAsync(string productId, int quantity, string transactionType, CancellationToken cancellationToken);
    }
}
