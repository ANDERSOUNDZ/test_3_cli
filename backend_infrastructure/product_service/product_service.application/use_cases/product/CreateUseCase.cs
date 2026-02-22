using product_service.domain.entities;
using product_service.ports.dtos.request.product;

namespace product_service
{
    public partial class ProductUseCase : IProductUseCase
    {
        public async Task<string> ExecuteAsync(ProductRequest request, CancellationToken cancellationToken)
        {
            var entity = new ProductEntity
            {
                Id = Guid.NewGuid().ToString("N"),
                Name = request.Name,
                Description = request.Description,
                CategoryId = request.CategoryId,
                Image = request.Image,
                Price = request.Price,
                Stock = request.Stock
            };
            await _productRepository.AddAsync(entity, cancellationToken);
            return entity.Id;
        }
    }
}
