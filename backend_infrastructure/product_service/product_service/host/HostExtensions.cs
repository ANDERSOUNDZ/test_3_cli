using product_service.host.extensions;
using Microsoft.AspNetCore.Builder;

namespace product_service.host
{
    public static class HostExtensions
    {
        public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
        {
            var services = builder.Services;
            var config = builder.Configuration;
            services.AddDatabaseSetup(config);
            services.AddSwaggerDocumentation();
            services.AddRepositories();
            services.AddUseCases();
            services.AddValidators();
            services.AddControllers();

            return builder;
        }
        public static WebApplication UseHexagonal(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Product_Service API v1");
                    options.RoutePrefix = "swagger";
                });
            }

            if (!app.Environment.IsDevelopment())
            {
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            app.UseAuthorization();
            app.MapControllers();
            return app;
        }
    }
}
