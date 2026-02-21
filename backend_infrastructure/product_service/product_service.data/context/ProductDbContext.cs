using Microsoft.EntityFrameworkCore;
using product_service.domain.entities;

namespace product_service.data.context
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }

        public DbSet<ProductEntity> Products => Set<ProductEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
