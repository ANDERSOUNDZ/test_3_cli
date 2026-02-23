using product_service.domain.entities;

namespace product_service
{
    public partial interface IProductRepository
    {
        Task<IEnumerable<ProductEntity>> GetAllProductsAsync(int? category, string? name, CancellationToken cancellationToken);
        Task<ProductEntity?> GetProductByIdAsync(string id, CancellationToken cancellationToken);
        Task AddProductAsync(ProductEntity product, CancellationToken cancellationToken);
        Task UpdateProductAsync(ProductEntity product, CancellationToken cancellationToken);
        Task DeleteProductAsync(string id, CancellationToken cancellationToken);
    }
}
