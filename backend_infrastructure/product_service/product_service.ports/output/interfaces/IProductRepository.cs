using product_service.domain.entities;

namespace product_service
{
    public partial interface IProductRepository
    {
        Task<ProductEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<ProductEntity>> GetAllAsync(string? category, string? name, CancellationToken cancellationToken);
        Task AddAsync(ProductEntity product, CancellationToken cancellationToken);
        Task UpdateAsync(ProductEntity product, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
