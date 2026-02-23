using product_service.ports.dtos.request.category;
using product_service.ports.dtos.response.category;

namespace product_service
{
    public partial interface IProductUseCase
    {
        Task<IEnumerable<CategoryResponse>> ExecuteAsync(CancellationToken cancellationToken);
        Task<CategoryResponse> ExecuteAsync(GetCategoryRequest id, CancellationToken cancellationToken);
        Task<string> ExecuteAsync(CategoryRequest request, CancellationToken cancellationToken);
        Task<bool> ExecuteAsync(int id, CategoryRequest request, CancellationToken cancellationToken);
        Task<bool> ExecuteAsync(DeleteCategoryRequest request, CancellationToken cancellationToken);
    }
}
