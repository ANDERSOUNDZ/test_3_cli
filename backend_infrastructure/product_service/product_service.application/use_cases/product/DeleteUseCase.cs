using product_service.ports.dtos.request.product;

namespace product_service
{
    public partial class ProductUseCase : IProductUseCase
    {
        public async Task<bool> ExecuteAsync(DeleteProductRequest request, CancellationToken cancellationToken)
        {
            var entity = await _productRepository.GetProductByIdAsync(request.Id, cancellationToken);

            if (entity == null) return false;

            await _productRepository.DeleteProductAsync(request.Id, cancellationToken);
            return true;
        }
    }
}
