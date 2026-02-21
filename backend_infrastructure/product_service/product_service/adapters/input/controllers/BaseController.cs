using Microsoft.AspNetCore.Mvc;
using product_service.ports.dtos.settings;
using product_service.ports.shared.dictionaries;
using product_service.ports.shared.enums;

namespace product_service.adapters.input.controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IActionResult OkResponse<T>(T data, ApiMessage message = ApiMessage.OperationSuccess)
            => Ok(new ApiResponse<T>(true, message.GetMessage(), data, (int)message));
        protected IActionResult BadRequestResponse(ApiMessage message = ApiMessage.BadRequest, object? errors = null)
            => BadRequest(new ApiResponse<object>(false, message.GetMessage(), errors, (int)message));
        protected IActionResult InternalErrorResponse(Exception? ex = null)
        {
            var msg = ex?.Message ?? ApiMessage.InternalServerError.GetMessage();
            return StatusCode(500, new ApiResponse<object>(false, msg, null, (int)ApiMessage.InternalServerError));
        }
    }
}
