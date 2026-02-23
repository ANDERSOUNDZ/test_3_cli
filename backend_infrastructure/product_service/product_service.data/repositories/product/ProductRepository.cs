
using Microsoft.EntityFrameworkCore;
using product_service.domain.entities;

namespace product_service
{
    public partial class ProductRepository : IProductRepository
    {
        public async Task<IEnumerable<ProductEntity>> GetAllProductsAsync(int? categoryId, string? name, CancellationToken cancellationToken)
        {
            var query = _context.Products.AsNoTracking().AsQueryable();

            if (categoryId.HasValue && categoryId > 0)
                query = query.Where(product => product.CategoryId == categoryId.Value);

            if (!string.IsNullOrEmpty(name))
                query = query.Where(product => product.Name.Contains(name));

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<ProductEntity?> GetProductByIdAsync(string id, CancellationToken cancellationToken)
        {
            return await _context.Products.FirstOrDefaultAsync(product => product.Id == id, cancellationToken);
        }

        public async Task AddProductAsync(ProductEntity product, CancellationToken cancellationToken)
        {
            await _context.Products.AddAsync(product, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateProductAsync(ProductEntity product, CancellationToken cancellationToken)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteProductAsync(string id, CancellationToken cancellationToken)
        {
            var product = await GetProductByIdAsync(id, cancellationToken);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
