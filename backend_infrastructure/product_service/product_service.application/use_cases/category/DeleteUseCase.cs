using product_service.ports.dtos.request.category;

namespace product_service
{
    public partial class ProductUseCase : IProductUseCase
    {
        public async Task<bool> ExecuteAsync(DeleteCategoryRequest request, CancellationToken cancellationToken)
        {
            var entity = await _productRepository.GetByIdCategoryAsync(request.Id, cancellationToken);

            if (entity == null) return false;

            await _productRepository.DeleteCategoryAsync(request.Id, cancellationToken);
            return true;
        }
    }
}
