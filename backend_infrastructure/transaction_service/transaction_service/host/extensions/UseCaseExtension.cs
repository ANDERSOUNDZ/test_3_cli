namespace transaction_service.host.extensions
{
    public static class UseCaseExtension
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<ITransactionUseCase, TransactionUseCase>();
            return services;
        }
    }
}
