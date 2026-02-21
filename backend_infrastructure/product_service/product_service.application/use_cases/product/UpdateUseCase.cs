using product_service.ports.dtos.request;

namespace product_service
{
    public partial class ProductUseCase : IProductUseCase
    {
        public async Task<bool> ExecuteAsync(Guid id, ProductRequest request, CancellationToken cancellationToken)
        {
            var entity = await _productRepository.GetByIdAsync(id, cancellationToken);

            if (entity == null) return false;

            entity.Name = request.Name;
            entity.Description = request.Description;
            entity.Category = request.Category;
            entity.Image = request.Image;
            entity.Price = request.Price;
            entity.Stock = request.Stock;

            await _productRepository.UpdateAsync(entity, cancellationToken);
            return true;
        }
    }
}
