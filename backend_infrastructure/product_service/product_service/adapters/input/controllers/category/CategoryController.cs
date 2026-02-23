using Microsoft.AspNetCore.Mvc;
using product_service.adapters.input.filters;
using product_service.ports.dtos.request.category;
using product_service.ports.dtos.request.product;
using product_service.ports.shared.enums;

namespace product_service.adapters.input.controllers.category
{
    [Route("/")]
    [ApiController]
    public class CategoryController : BaseController
    {
        private readonly IProductUseCase _executor;
        public CategoryController(IProductUseCase executor)
        {
            _executor = executor;
        }

        [HttpGet("list_categories")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _executor.ExecuteAsync(cancellationToken);
            return OkResponse(result);
        }

        [HttpGet("get_categoryt/{id}")]
        public async Task<IActionResult> GetById(
            int id,
            CancellationToken cancellationToken = default)
        {
            var result = await _executor.ExecuteAsync(new GetCategoryRequest(id), cancellationToken);

            if (result == null)
                return BadRequestResponse(ApiMessage.BadRequestCategory);

            return OkResponse(result);
        }

        [HttpPost("create_category")]
        [ServiceFilter(typeof(ValidationFilter<CategoryRequest>))]
        public async Task<IActionResult> Create([FromBody] CategoryRequest request, CancellationToken cancellationToken)
        {
            var id = await _executor.ExecuteAsync(request, cancellationToken);
            return OkResponse(id, ApiMessage.CategorySuccess);
        }
        [HttpPut("update_category/{id}")]
        [ServiceFilter(typeof(ValidationFilter<CategoryRequest>))]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _executor.ExecuteAsync(id, request, cancellationToken);
                if (!result)
                    return BadRequestResponse(ApiMessage.BadRequestProductUpdate);
                return OkResponse(true, ApiMessage.UpdateCategorySuccess);
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

        [HttpDelete("delete_category/{id}")]
        public async Task<IActionResult> Delete(
            int id,
            CancellationToken cancellationToken = default)
        {
            var result = await _executor.ExecuteAsync(new DeleteCategoryRequest(id), cancellationToken);

            if (!result)
                return BadRequestResponse(ApiMessage.BadRequestDeleteCategory);

            return OkResponse(true, ApiMessage.DeleteCategorySuccess);
        }
    }
}
