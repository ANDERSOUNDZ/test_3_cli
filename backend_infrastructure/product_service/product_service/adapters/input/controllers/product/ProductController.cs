using Microsoft.AspNetCore.Mvc;
using product_service.adapters.input.filters;
using product_service.ports.dtos.request.product;
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
            return OkResponse(result, ApiMessage.ProductSuccess);
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
            string id,
            CancellationToken cancellationToken = default)
        {
            var result = await _executor.ExecuteAsync(new GetProductRequest(id), cancellationToken);

            if (result == null)
                return BadRequestResponse(ApiMessage.BadRequestProduct);

            return OkResponse(result);
        }

        [HttpPut("update_product/{id}")]
        [ServiceFilter(typeof(ValidationFilter<ProductRequest>))]
        public async Task<IActionResult> Update(string id, [FromBody] ProductRequest request,CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _executor.ExecuteAsync(id, request, cancellationToken);
                if (!result)
                    return BadRequestResponse(ApiMessage.BadRequestProductUpdate);
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

        [HttpPut("update_stock/{id}/{quantity}/{isIncrement}")]
        public async Task<IActionResult> UpdateStock(string id, int quantity, bool isIncrement, CancellationToken ct)
        {
            var result = await _executor.ExecuteAsync(id, quantity, isIncrement, ct);

            if (!result) return BadRequest(new { message = "No se pudo actualizar el stock." });

            return Ok(new { message = "Stock actualizado correctamente." });
        }

        [HttpDelete("delete_product/{id}")]
        public async Task<IActionResult> Delete(
            string id,
            CancellationToken cancellationToken = default)
        {
            var result = await _executor.ExecuteAsync(new DeleteProductRequest(id), cancellationToken);

            if (!result)
                return BadRequestResponse(ApiMessage.BadRequestDeleteProduct);

            return OkResponse(true, ApiMessage.OperationSuccess);
        }
    }
}
