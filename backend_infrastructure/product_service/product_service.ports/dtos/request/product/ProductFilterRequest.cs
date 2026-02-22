namespace product_service.ports.dtos.request.product
{
    public class ProductFilterRequest
    {
        public int? CategoryId { get; set; }
        public string? Name { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 15;
    }
}
