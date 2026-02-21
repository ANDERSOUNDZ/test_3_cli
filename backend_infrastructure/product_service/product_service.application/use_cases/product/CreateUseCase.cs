using product_service.domain.entities;
using product_service.ports.dtos.request;

namespace product_service
{
    public partial class ProductUseCase : IProductUseCase
    {
        public async Task<Guid> ExecuteAsync(ProductRequest request, CancellationToken cancellationToken)
        {
            var entity = new ProductEntity
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                Category = request.Category,
                Image = request.Image,
                Price = request.Price,
                Stock = request.Stock
            };

            await _productRepository.AddAsync(entity, cancellationToken);
            return entity.Id;
        }
    }
}
