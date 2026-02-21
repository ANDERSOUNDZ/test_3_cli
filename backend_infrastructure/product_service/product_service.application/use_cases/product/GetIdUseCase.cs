using product_service.ports.dtos.request;
using product_service.ports.dtos.response;

namespace product_service
{
    public partial class ProductUseCase : IProductUseCase
    {
        public async Task<ProductResponse?> ExecuteAsync(GetProductRequest request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);

            if (product == null) return null;

            return new ProductResponse
            {
                Name = product.Name,
                Description = product.Description,
                Category = product.Category,
                Image = product.Image,
                Price = product.Price,
                Stock = product.Stock
            };
        }
    }
}
