namespace product_service
{
    public partial class ProductUseCase : IProductUseCase
    {
        public async Task<bool> ExecuteAsync(string id, int quantity, bool isIncrement, CancellationToken cancellationToken)
        {
            var entity = await _productRepository.GetByIdAsync(id, cancellationToken);
            if (entity == null) return false;

            entity.UpdateStock(quantity, isIncrement);
            await _productRepository.UpdateAsync(entity, cancellationToken);
            return true;
        }
    }
}
