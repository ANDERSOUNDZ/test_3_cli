using product_service.ports.dtos.request.category;
using product_service.ports.dtos.response.category;

namespace product_service
{
    public partial class ProductUseCase : IProductUseCase
    {
        public async Task<CategoryResponse> ExecuteAsync(GetCategoryRequest request, CancellationToken cancellationToken)
        {
            var category = await _productRepository.GetByIdCategoryAsync(request.Id, cancellationToken);
            if (category == null)
            {
                throw new Exception("Category not found");
            }
            return new CategoryResponse(category.Id, category.Name, category.Description);
        }
    }
}
