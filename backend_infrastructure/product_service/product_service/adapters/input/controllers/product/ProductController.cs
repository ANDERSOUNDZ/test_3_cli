using Microsoft.AspNetCore.Mvc;
using product_service.adapters.input.filters;
using product_service.ports.dtos.request;
using product_service.ports.shared.enums;

namespace product_service.adapters.input.controllers.product
{
    [Route("product_service/")]
    [ApiController]
    public class ProductController : BaseController
    {
        private readonly IProductUseCase _executor;
        public ProductController(IProductUseCase executor)
        {
            _executor = executor;
        }
        [HttpPost("create_product")]
        [ServiceFilter(typeof(ValidationFilter<ProductRequest>))]
        public async Task<IActionResult> CreateProduct(
        [FromBody] ProductRequest request,
        CancellationToken cancellationToken = default)
        {
            var result = await _executor.ExecuteAsync(request, cancellationToken);
            return OkResponse(result, ApiMessage.RegisterSuccess);
        }
        [HttpGet("list_products")]
        public async Task<IActionResult> GetAll(
            [FromQuery] ProductFilterRequest request,
            CancellationToken cancellationToken = default)
        {
            var result = await _executor.ExecuteAsync(request, cancellationToken);
            return OkResponse(result);
        }

        [HttpGet("get_product/{id}")]
        public async Task<IActionResult> GetById(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var result = await _executor.ExecuteAsync(new GetProductRequest(id), cancellationToken);

            if (result == null)
                return BadRequestResponse(ApiMessage.BadRequest, "El producto no existe.");

            return OkResponse(result);
        }

        [HttpPut("update_product/{id}")]
        [ServiceFilter(typeof(ValidationFilter<ProductRequest>))]
        public async Task<IActionResult> Update(Guid id,[FromBody] ProductRequest request,CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _executor.ExecuteAsync(id, request, cancellationToken);
                if (!result)
                    return BadRequestResponse(ApiMessage.BadRequest, "No se pudo actualizar. Producto no encontrado.");
                return OkResponse(true, ApiMessage.OperationSuccess);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequestResponse(ApiMessage.ValidationError, ex.Message);
            }
            catch (Exception ex)
            {
                return InternalErrorResponse(ex);
            }
        }

        [HttpDelete("delete_product/{id}")]
        public async Task<IActionResult> Delete(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var result = await _executor.ExecuteAsync(new DeleteProductRequest(id), cancellationToken);

            if (!result)
                return BadRequestResponse(ApiMessage.BadRequest, "No se pudo eliminar. Producto no encontrado.");

            return OkResponse(true, ApiMessage.OperationSuccess);
        }
    }
}
