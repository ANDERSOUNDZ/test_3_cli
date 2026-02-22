using product_service.ports.dtos.response.category;

namespace product_service
{ 
    public partial class ProductUseCase : IProductUseCase
    {
        public async Task<IEnumerable<CategoryResponse>> ExecuteAsync(CancellationToken cancellationToken)
        {
            var categories = await _productRepository.GetAllCategoryAsync(cancellationToken);
            return categories.Select(category => new CategoryResponse(category.Id, category.Name, category.Description));
        }
    }
}
