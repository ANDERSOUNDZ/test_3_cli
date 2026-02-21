using Microsoft.OpenApi;

namespace transaction_service.host.extensions
{
    public static class SwaggerExtension
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Transaction Services API",
                    Version = "v1",
                    Description = "Documentación de la API de Transacion Services",
                    Contact = new OpenApiContact
                    {
                        Name = "Anderson Chanchay",
                        Email = "andersonmikol@live.com"
                    }
                });
            });
            return services;
        }
    }
}
