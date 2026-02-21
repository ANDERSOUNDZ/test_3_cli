namespace product_service.ports.dtos.request
{
    public class ProductFilterRequest
    {
        public string? Category { get; set; }
        public string? Name { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 15;
    }
}
