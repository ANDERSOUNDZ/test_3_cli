using Microsoft.EntityFrameworkCore;
using product_service.data.context;

namespace product_service.host.extensions
{
    public static class DatabaseExtension
    {
        public static IServiceCollection AddDatabaseSetup(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ProductDbContext>(options =>
            {
                options.UseSqlServer(connectionString, sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(30), null);
                    sqlOptions.MigrationsAssembly("product_service.data");
                });

                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                {
                    options.EnableDetailedErrors();
                    options.EnableSensitiveDataLogging();
                }
            });
            return services;
        }
    }
}
