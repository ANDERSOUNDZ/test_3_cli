using System.Net.Http.Json;
using System.Text.Json;

namespace transaction_service
{
    public partial class ProductClient : IProductClient
    {
        public async Task<(bool Success, string ProductName, string Message)> UpdateProductStockAsync(
            string productId, int quantity, string transactionType, CancellationToken cancellationToken)
        {
            var responseGet = await _httpClient.GetAsync($"product_service/get_product/{productId}", cancellationToken);

            if (!responseGet.IsSuccessStatusCode)
                return (false, string.Empty, "Producto no encontrado.");

            var productData = await responseGet.Content.ReadFromJsonAsync<JsonElement>(cancellationToken: cancellationToken);

            string productName = "Desconocido";
            if (productData.TryGetProperty("data", out var dataElement))
            {
                if (dataElement.TryGetProperty("name", out var nameElement))
                {
                    productName = nameElement.GetString() ?? "Desconocido";
                }
            }
            bool isIncrement = transactionType.ToLower() == "compra" || transactionType.ToLower() == "entrada";

            var responseUpdate = await _httpClient.PutAsync(
                $"product_service/update_stock/{productId}/{quantity}/{isIncrement}",
                null,
                cancellationToken);

            if (!responseUpdate.IsSuccessStatusCode)
                return (false, string.Empty, "Error al actualizar stock.");

            return (true, productName, "OK");
        }
    }
}
