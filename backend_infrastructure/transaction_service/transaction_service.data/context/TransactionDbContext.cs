using Microsoft.EntityFrameworkCore;

namespace transaction_service.data.context
{
    public class TransactionDbContext : DbContext
    {
        public TransactionDbContext(DbContextOptions<TransactionDbContext> options) : base(options) { }

        //public DbSet<TransactionEntity> Products => Set<TransactionEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TransactionDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
