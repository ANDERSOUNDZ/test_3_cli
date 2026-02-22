using Microsoft.EntityFrameworkCore;
using transaction_service.domain.entities;

namespace transaction_service.data.context
{
    public class TransactionDbContext : DbContext
    {
        public TransactionDbContext(DbContextOptions<TransactionDbContext> options) : base(options) { }

        public DbSet<TransactionEntity> Transactions => Set<TransactionEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TransactionDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
