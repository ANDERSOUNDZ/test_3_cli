namespace product_service.domain.entities
{
    public class ProductEntity
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public void UpdateStock(int quantity, bool isIncrement)
        {
            if (!isIncrement && Stock < quantity)
                throw new InvalidOperationException("Stock insuficiente.");
            Stock = isIncrement ? Stock + quantity : Stock - quantity;
        }
    }
}
