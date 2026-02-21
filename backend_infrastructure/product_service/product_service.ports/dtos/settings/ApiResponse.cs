namespace product_service.ports.dtos.settings
{
    public class ApiResponse<Response>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public Response? Data { get; set; }
        public int? Code { get; set; }
        public ApiResponse(bool success, string message, Response? data = default, int? code = null)
        {
            Success = success;
            Message = message;
            Data = data;
            Code = code;
        }
    }
}