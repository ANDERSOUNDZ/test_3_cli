namespace transaction_service.ports.dtos.response
{
    public class TransactionResponse
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string TransactionType { get; set; } = string.Empty;
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public string Detail { get; set; } = string.Empty;
    }
}
