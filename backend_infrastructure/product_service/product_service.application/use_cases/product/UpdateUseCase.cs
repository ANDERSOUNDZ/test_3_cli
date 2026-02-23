using product_service.ports.dtos.request.product;

namespace product_service
{
    public partial class ProductUseCase : IProductUseCase
    {
        public async Task<bool> ExecuteAsync(string id, ProductRequest request, CancellationToken cancellationToken)
        {
            var entity = await _productRepository.GetProductByIdAsync(id, cancellationToken);

            if (entity == null) return false;

            entity.Name = request.Name;
            entity.Description = request.Description;
            entity.CategoryId = request.CategoryId;
            entity.Image = request.Image;
            entity.Price = request.Price;

            if (request.Stock < entity.Stock)
            {
                int diferencia = entity.Stock - request.Stock;
                entity.UpdateStock(diferencia, false);
            }
            else
            {
                entity.Stock = request.Stock;
            }

            await _productRepository.UpdateProductAsync(entity, cancellationToken);
            return true;
        }
    }
}
