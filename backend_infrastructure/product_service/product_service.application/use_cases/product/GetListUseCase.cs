using product_service.ports.dtos.request.product;
using product_service.ports.dtos.response.product;

namespace product_service
{
    public partial class ProductUseCase : IProductUseCase
    {
        public async Task<IEnumerable<ProductResponse>> ExecuteAsync(ProductFilterRequest request, CancellationToken cancellationToken)
        {
            int pageSize = request.PageSize > 15 ? 15 : (request.PageSize <= 0 ? 15 : request.PageSize);
            int skip = (request.Page - 1) * pageSize;

            var products = await _productRepository.GetAllProductsAsync(request.CategoryId, request.Name, cancellationToken);

            return products
                .Skip(skip)
                .Take(pageSize)
                .Select(product => new ProductResponse
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    CategoryId = product.CategoryId,
                    Image = product.Image,
                    Price = product.Price,
                    Stock = product.Stock
                });
        }
    }
}
