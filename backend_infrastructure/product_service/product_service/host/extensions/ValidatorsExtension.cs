using FluentValidation;
using product_service.adapters.input.filters;
using product_service.adapters.input.validators.category;
using product_service.adapters.input.validators.product;
using product_service.ports.dtos.request.category;
using product_service.ports.dtos.request.product;

namespace product_service.host.extensions
{
    public static class ValidatorsExtension
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<CreateProductValidator>();
            services.AddScoped<ValidationFilter<ProductRequest>>();
            services.AddValidatorsFromAssemblyContaining<CreateCategoryValidator>();
            services.AddScoped<ValidationFilter<CategoryRequest>>();
            return services;
        }
    }
}
