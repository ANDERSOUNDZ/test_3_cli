using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace transaction_service.data.context
{
    internal class TransactionDbContextFactory : IDesignTimeDbContextFactory<TransactionDbContext>
    {
        public TransactionDbContext CreateDbContext(string[] args)
        {
            var apiProjectPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "../product-api"));

            var configuration = new ConfigurationBuilder()
                .SetBasePath(apiProjectPath)
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' no encontrada.");

            var optionsBuilder = new DbContextOptionsBuilder<TransactionDbContext>();
            optionsBuilder.UseSqlServer(connectionString, sql =>
            {
                sql.MigrationsAssembly("transaction_service.data");
            });

            return new TransactionDbContext(optionsBuilder.Options);
        }
    }
}
