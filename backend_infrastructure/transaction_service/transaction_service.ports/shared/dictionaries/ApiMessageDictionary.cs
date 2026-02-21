using transaction_service.ports.shared.enums;

namespace transaction_service.ports.shared.dictionaries
{
    public static class ApiMessageDictionary
    {
        private static readonly Dictionary<ApiMessage, string> Messages = new()
        {
            [ApiMessage.TransactionSuccess] = "Transacción realizada.",
            [ApiMessage.BadRequest] = "Solicitud inválida o cuerpo vacío.",
            [ApiMessage.ValidationError] = "Error de validación.",
            [ApiMessage.InternalServerError] = "Error interno del servidor.",
        };
        public static string GetMessage(this ApiMessage message)
            => Messages.TryGetValue(message, out var msg) ? msg : "Mensaje no definido.";
    }
}
