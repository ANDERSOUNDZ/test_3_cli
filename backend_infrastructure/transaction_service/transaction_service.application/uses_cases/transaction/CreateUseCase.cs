using System.Net.Http.Json;
using transaction_service.domain.entities;
using transaction_service.ports.dtos.request;

namespace transaction_service
{
    public partial class TransactionUseCase : ITransactionUseCase
    {
        public async Task<string> ExecuteAsync(TransactionRequest request, CancellationToken cancellationToken)
        {
            var (success, productName, errorMessage) = await _productClient.UpdateProductStockAsync(
                request.ProductId, request.Quantity, request.TransactionType, cancellationToken);

            if (!success)
            {
                throw new InvalidOperationException(errorMessage);
            }

            var entity = new TransactionEntity
            {
                Id = Guid.NewGuid().ToString("N"),
                Date = DateTime.UtcNow,
                TransactionType = request.TransactionType,
                ProductId = request.ProductId,
                ProductName = productName,
                Quantity = request.Quantity,
                UnitPrice = request.UnitPrice,
                TotalPrice = request.Quantity * request.UnitPrice,
                Detail = request.Detail
            };

            await _transactionRepository.AddTransactionAsync(entity, cancellationToken);
            return entity.Id;
        }        
    }
}