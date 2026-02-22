
namespace transaction_service.ports.dtos.request
{
    public record TransactionFilterRequest(
        DateTime? StartDate,
        DateTime? EndDate,
        string? Type,
        int Page = 1,
        int PageSize = 15);
}
