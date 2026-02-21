using Microsoft.AspNetCore.Mvc;

namespace transaction_service.adapters.input.controllers.transaction
{
    [Route("transaction_service/")]
    [ApiController]
    public class TransactionController : BaseController
    {
        private readonly ITransactionUseCase _executor;
        public TransactionController(ITransactionUseCase executor)
        {
            _executor = executor;
        }
    }
}
