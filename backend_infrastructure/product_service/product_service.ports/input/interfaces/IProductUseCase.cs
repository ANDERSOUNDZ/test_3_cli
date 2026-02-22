using product_service.ports.dtos.request;
using product_service.ports.dtos.response;

namespace product_service
{
    public partial interface IProductUseCase
    {
        Task<string> ExecuteAsync(ProductRequest request, CancellationToken cancellationToken);
        Task<IEnumerable<ProductResponse>> ExecuteAsync(ProductFilterRequest request, CancellationToken cancellationToken);
        Task<ProductResponse?> ExecuteAsync(GetProductRequest request, CancellationToken cancellationToken);
        Task<bool> ExecuteAsync(string id, ProductRequest request, CancellationToken cancellationToken);
        Task<bool> ExecuteAsync(DeleteProductRequest request, CancellationToken cancellationToken);
    }
}
