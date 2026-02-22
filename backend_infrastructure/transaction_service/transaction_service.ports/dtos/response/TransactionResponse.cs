namespace transaction_service.ports.dtos.response
{
    public class TransactionResponse
    {
        public string Id { get; set; } = string.Empty;
        public string ProductId { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string TransactionType { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public string Detail { get; set; } = string.Empty;
    }
}
