using product_service.domain.entities;

namespace product_service
{
    public partial interface IProductRepository
    {
        Task<IEnumerable<CategoryEntity>> GetAllCategoryAsync(CancellationToken cancellationToken);
        Task<CategoryEntity?> GetByIdCategoryAsync(int id, CancellationToken cancellationToken);
        Task AddCategoryAsync(CategoryEntity category, CancellationToken cancellationToken);
    }
}
