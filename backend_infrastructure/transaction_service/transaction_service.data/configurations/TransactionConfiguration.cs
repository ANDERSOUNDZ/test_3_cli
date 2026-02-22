using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using transaction_service.domain.entities;

namespace transaction_service.data.configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<TransactionEntity>
    {
        public void Configure(EntityTypeBuilder<TransactionEntity> builder)
        {
            builder.ToTable("tb_transaction");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).IsRequired().HasMaxLength(32).IsUnicode(false);
            builder.Property(t => t.Date).IsRequired();
            builder.Property(t => t.TransactionType).IsRequired().HasMaxLength(50);
            builder.Property(t => t.ProductId).IsRequired().HasMaxLength(32).IsUnicode(false);
            builder.HasIndex(t => t.ProductId);
            builder.Property(t => t.Quantity).IsRequired();
            builder.Property(t => t.UnitPrice).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(t => t.TotalPrice).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(t => t.ProductName).IsRequired().HasMaxLength(200);
            builder.Property(t => t.Detail).HasMaxLength(500);
        }
    }
}