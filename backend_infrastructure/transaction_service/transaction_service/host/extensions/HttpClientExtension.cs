namespace transaction_service.host.extensions
{
    public static class HttpClientExtension
    {
        public static IServiceCollection AddExternalClients(this IServiceCollection services, IConfiguration config)
        {
            var productUrl = config["ExternalServices:ProductServiceUrl"] ?? "http://product-service:8080/";
            services.AddHttpClient<IProductClient, ProductClient>(client =>
            {
                client.BaseAddress = new Uri(productUrl);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });
            return services;
        }
    }
}
