namespace transaction_service.ports.dtos.request
{
    public class TransactionRequest
    {
        public string TransactionType { get; set; } = string.Empty;
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string Detail { get; set; } = string.Empty;
    }
}
