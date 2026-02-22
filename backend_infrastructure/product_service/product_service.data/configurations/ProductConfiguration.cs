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
            builder.HasKey(product => product.Id);
            builder.Property(product => product.Id).HasMaxLength(36).IsRequired();
            builder.Property(product => product.Name).IsRequired().HasMaxLength(200);
            builder.Property(product => product.Description).HasMaxLength(1000);
            builder.Property(product => product.Image).HasMaxLength(500);
            builder.Property(product => product.Price).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(product => product.Stock) .IsRequired();
            builder.Property(product => product.CategoryId).IsRequired();
            builder.HasOne(product => product.Category)
                .WithMany(category => category.Products)
                .HasForeignKey(product => product.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
