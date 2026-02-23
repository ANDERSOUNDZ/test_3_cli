using product_service.ports.dtos.request.category;

namespace product_service
{
    public partial class ProductUseCase : IProductUseCase
    {
        public async Task<bool> ExecuteAsync(int id, CategoryRequest request, CancellationToken cancellationToken)
        {
            var entity = await _productRepository.GetByIdCategoryAsync(id, cancellationToken);

            if (entity == null) return false;

            entity.Name = request.Name;
            entity.Description = request.Description;

            await _productRepository.UpdateCategoryAsync(entity, cancellationToken);
            return true;
        }
    }
}
