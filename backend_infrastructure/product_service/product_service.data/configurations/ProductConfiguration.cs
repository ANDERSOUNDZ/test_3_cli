using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using product_service.domain.entities;

namespace product_service.data.configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.ToTable("tb_product");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                   .HasMaxLength(32)
                   .IsRequired();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(200);
            builder.Property(p => p.Description).HasMaxLength(1000);
            builder.Property(p => p.Category).HasMaxLength(100);
            builder.Property(p => p.Image).HasMaxLength(500);
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(p => p.Stock).IsRequired();
        }
    }
}
