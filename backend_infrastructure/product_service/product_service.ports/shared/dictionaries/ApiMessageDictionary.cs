using product_service.ports.shared.enums;

namespace product_service.ports.shared.dictionaries
{
    public static class ApiMessageDictionary
    {
        private static readonly Dictionary<ApiMessage, string> Messages = new()
        {
            [ApiMessage.ProductSuccess] = "Producto registrado correctamente.",
            [ApiMessage.CategorySuccess] = "Categoria registrada correctamente.",
            [ApiMessage.BadRequest] = "Solicitud inválida o cuerpo vacío.",
            [ApiMessage.BadRequestProduct] = "El producto no existe.",
            [ApiMessage.BadRequestProductUpdate] = "No se pudo actualizar. Producto no encontrado.",
            [ApiMessage.BadRequestDeleteProduct] = "No se pudo eliminar. Producto no encontrado.",
            [ApiMessage.ValidationError] = "Error de validación.",
            [ApiMessage.InternalServerError] = "Error interno del servidor.",
            [ApiMessage.BadRequestCategory] = "No se encontró la categoría solicitada.",
            [ApiMessage.BadRequestDeleteCategory] = "No se pudo eliminar. Categoria no encontrado.",
            [ApiMessage.DeleteCategorySuccess] = "Categoria eliminada correctamente.",
            [ApiMessage.UpdateCategorySuccess] = "Categoria actualizada correctamente.",
            [ApiMessage.BadRequestStock] = "Stock insuficiente, verifique con administración.",
        };
        public static string GetMessage(this ApiMessage message)
            => Messages.TryGetValue(message, out var msg) ? msg : "Mensaje no definido.";
    }
}
