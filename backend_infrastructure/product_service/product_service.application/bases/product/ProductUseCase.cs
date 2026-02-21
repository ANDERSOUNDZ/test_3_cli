namespace product_service
{
    public partial class ProductUseCase : IProductUseCase
    {
        private readonly IProductRepository _productRepository;
        public ProductUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
    }
}
