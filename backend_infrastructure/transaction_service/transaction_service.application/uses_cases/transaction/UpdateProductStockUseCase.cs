using System.Net.Http.Json;

namespace transaction_service
{
    public partial class ProductClient : IProductClient
    {
        public async Task<(bool Success, string ProductName, string Message)> UpdateProductStockAsync(
            string productId, int quantity, string transactionType, CancellationToken cancellationToken)
        {
            // Lógica para obtener el nombre y actualizar stock en el Product Service
            var response = await _httpClient.GetAsync($"product_service/get_product/{productId}", cancellationToken);

            if (!response.IsSuccessStatusCode)
                return (false, string.Empty, "Producto no encontrado.");

            var productData = await response.Content.ReadFromJsonAsync<dynamic>(cancellationToken);
            string productName = productData?.data?.name ?? "Desconocido";

            // Lógica de actualización de stock (PUT)
            bool isIncrement = transactionType.ToLower() == "compra" || transactionType.ToLower() == "entrada";
            var responseUpdate = await _httpClient.PutAsync($"product_service/update_stock/{productId}/{quantity}/{isIncrement}", null, cancellationToken);

            if (!responseUpdate.IsSuccessStatusCode)
                return (false, string.Empty, "Error al actualizar stock.");

            return (true, productName, "OK");
        }
    }
}
