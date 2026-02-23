using Microsoft.EntityFrameworkCore;
using product_service.domain.entities;
namespace product_service
{
    public partial class ProductRepository : IProductRepository
    {
        public async Task<IEnumerable<CategoryEntity>> GetAllCategoryAsync(CancellationToken cancellationToken)
        {
            return await _context.Categories
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<CategoryEntity?> GetByIdCategoryAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(category => category.Id == id, cancellationToken);
        }

        public async Task AddCategoryAsync(CategoryEntity category, CancellationToken cancellationToken)
        {
            await _context.Categories.AddAsync(category, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task UpdateCategoryAsync(CategoryEntity category, CancellationToken cancellationToken)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task DeleteCategoryAsync(int id, CancellationToken cancellationToken)
        {
            var category = await GetByIdCategoryAsync(id, cancellationToken);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
