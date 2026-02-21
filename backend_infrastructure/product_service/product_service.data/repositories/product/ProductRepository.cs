
using Microsoft.EntityFrameworkCore;
using product_service.domain.entities;

namespace product_service
{
    public partial class ProductRepository : IProductRepository
    {
        public async Task<IEnumerable<ProductEntity>> GetAllAsync(string? category, string? name, CancellationToken cancellationToken)
        {
            var query = _context.Products.AsNoTracking().AsQueryable();

            if (!string.IsNullOrEmpty(category))
                query = query.Where(p => p.Category == category);

            if (!string.IsNullOrEmpty(name))
                query = query.Where(p => p.Name.Contains(name));

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<ProductEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        public async Task AddAsync(ProductEntity product, CancellationToken cancellationToken)
        {
            await _context.Products.AddAsync(product, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(ProductEntity product, CancellationToken cancellationToken)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var product = await GetByIdAsync(id, cancellationToken);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
