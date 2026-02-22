using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using transaction_service.domain.entities;

namespace transaction_service.data.configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<TransactionEntity>
    {
        public void Configure(EntityTypeBuilder<TransactionEntity> transaction)
        {
            transaction.ToTable("tb_transaction");
            transaction.HasKey(t => t.Id);
            transaction.Property(t => t.Id).IsRequired();
            transaction.Property(t => t.Date).IsRequired();
            transaction.Property(t => t.TransactionType)
                .IsRequired()
                .HasMaxLength(50);
            transaction.Property(t => t.ProductId).IsRequired();
            transaction.Property(t => t.Quantity).IsRequired();
            transaction.Property(t => t.UnitPrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
            transaction.Property(t => t.TotalPrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
            transaction.Property(t => t.ProductName)
                .IsRequired()
                .HasMaxLength(200);
            transaction.Property(t => t.Detail)
                .HasMaxLength(500);
        }
    }
}
