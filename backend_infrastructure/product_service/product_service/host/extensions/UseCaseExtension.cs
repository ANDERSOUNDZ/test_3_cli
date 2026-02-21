namespace product_service.host.extensions
{
    public static class UseCaseExtension
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<IProductUseCase, ProductUseCase>();
            return services;
        }
    }
}
