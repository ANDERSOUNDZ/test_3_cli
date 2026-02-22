using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using product_service.adapters.input.filters;
using product_service.ports.dtos.request.category;
using product_service.ports.dtos.response.category;
using product_service.ports.shared.enums;

namespace product_service.adapters.input.controllers.category
{
    public class ProductController : BaseController
    {
        private readonly IProductUseCase _executor;
        public ProductController(IProductUseCase executor)
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
    }
}
