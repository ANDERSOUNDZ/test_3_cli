using Microsoft.AspNetCore.Mvc;
using transaction_service.adapters.input.filters;
using transaction_service.ports.dtos.request;
using transaction_service.ports.shared.enums;

namespace transaction_service.adapters.input.controllers.transaction
{
    [Route("/")]
    [ApiController]
    public class TransactionController : BaseController
    {
        private readonly ITransactionUseCase _executor;
        public TransactionController(ITransactionUseCase executor)
        {
            _executor = executor;
        }

        [HttpPost("register_transaction")]
        [ServiceFilter(typeof(ValidationFilter<TransactionRequest>))]
        public async Task<IActionResult> Create(
            [FromBody] TransactionRequest request,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _executor.ExecuteAsync(request, cancellationToken);
                return OkResponse(result, ApiMessage.TransactionSuccess);
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

        [HttpGet("list_transactions")]
        public async Task<IActionResult> GetHistory(
            [FromQuery] TransactionFilterRequest request,
            CancellationToken cancellationToken = default)
        {
            var result = await _executor.ExecuteAsync(request, cancellationToken);
            return OkResponse(result);
        }
    }
}
