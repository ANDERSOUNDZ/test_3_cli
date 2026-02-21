using product_service.ports.dtos.request;
using product_service.ports.dtos.response;

namespace product_service
{
    public partial class ProductUseCase : IProductUseCase
    {
        public async Task<IEnumerable<ProductResponse>> ExecuteAsync(ProductFilterRequest request, CancellationToken cancellationToken)
        {
            int pageSize = request.PageSize > 15 ? 15 : (request.PageSize <= 0 ? 15 : request.PageSize);
            int skip = (request.Page - 1) * pageSize;

            var products = await _productRepository.GetAllAsync(request.Category, request.Name, cancellationToken);

            return products
                .Skip(skip)
                .Take(pageSize)
                .Select(product => new ProductResponse
                {
                    Name = product.Name,
                    Description = product.Description,
                    Category = product.Category,
                    Image = product.Image,
                    Price = product.Price,
                    Stock = product.Stock
                });
        }
    }
}
