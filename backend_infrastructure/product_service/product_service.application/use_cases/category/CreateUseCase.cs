using product_service.domain.entities;
using product_service.ports.dtos.request.category;

namespace product_service
{
    public partial class ProductUseCase : IProductUseCase
    {
        public async Task<string> ExecuteAsync(CategoryRequest request, CancellationToken cancellationToken)
        {
            var entity = new CategoryEntity
            {
                Name = request.Name,
                Description = request.Description
            };
            await _productRepository.AddCategoryAsync(entity, cancellationToken);
            return entity.Name;
        }
    }
}
