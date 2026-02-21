using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using product_service.domain.entities;

namespace product_service.data.configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> product)
        {
            product.ToTable("tb_product");
            product.HasKey(p => p.Id);
            product.Property(p => p.Id).IsRequired();
            product.Property(p => p.Name).IsRequired().HasMaxLength(200);
            product.Property(p => p.Description).HasMaxLength(1000);
            product.Property(p => p.Category).HasMaxLength(100);
            product.Property(p => p.Image).HasMaxLength(500);
            product.Property(p => p.Price).HasColumnType("decimal(18,2)").IsRequired();
            product.Property(p => p.Stock).IsRequired();
        }
    }
}
