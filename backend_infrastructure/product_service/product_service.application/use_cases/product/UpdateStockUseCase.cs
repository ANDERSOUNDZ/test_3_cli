namespace product_service
{
    public partial class ProductUseCase : IProductUseCase
    {
        public async Task<bool> ExecuteAsync(string id, int quantity, bool isIncrement, CancellationToken cancellationToken)
        {
            var entity = await _productRepository.GetProductByIdAsync(id, cancellationToken);
            if (entity == null) return false;

            entity.UpdateStock(quantity, isIncrement);
            await _productRepository.UpdateProductAsync(entity, cancellationToken);
            return true;
        }
    }
}
