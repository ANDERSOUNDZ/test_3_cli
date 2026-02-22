namespace transaction_service.ports.dtos.request
{
    public class TransactionRequest
    {
        public string TransactionType { get; set; } = string.Empty;
        public string ProductId { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string Detail { get; set; } = string.Empty;
    }
}
