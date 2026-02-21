namespace product_service.ports.dtos.request
{
    public class ProductRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
