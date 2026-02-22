namespace transaction_service.host.extensions
{
    public static class HttpClientExtension
    {
        public static IServiceCollection AddExternalClients(this IServiceCollection services, IConfiguration config)
        {
            var productUrl = config["ExternalServices:ProductServiceUrl"] ?? "http://localhost:8081/";
            services.AddHttpClient<IProductClient, ProductClient>(client =>
            {
                client.BaseAddress = new Uri(productUrl);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });
            return services;
        }
    }
}
