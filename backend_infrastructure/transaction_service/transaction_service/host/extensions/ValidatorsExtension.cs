using FluentValidation;
using transaction_service.adapters.input.filters;
using transaction_service.adapters.input.validators.transaction;
using transaction_service.ports.dtos.request;

namespace transaction_service.host.extensions
{
    public static class ValidatorsExtension
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<TransactionValidator>();
            services.AddScoped<ValidationFilter<TransactionRequest>>();
            return services;
        }
    }
}