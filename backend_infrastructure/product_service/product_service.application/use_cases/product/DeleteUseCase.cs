using product_service.ports.dtos.request;

namespace product_service
{
    public partial class ProductUseCase : IProductUseCase
    {
        public async Task<bool> ExecuteAsync(DeleteProductRequest request, CancellationToken cancellationToken)
        {
            var entity = await _productRepository.GetByIdAsync(request.Id, cancellationToken);

            if (entity == null) return false;

            await _productRepository.DeleteAsync(request.Id, cancellationToken);
            return true;
        }
    }
}
