
namespace transaction_service.domain.entities
{
    public class TransactionEntity
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string TransactionType { get; set; } = string.Empty;
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public string Detail { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
    }
}
