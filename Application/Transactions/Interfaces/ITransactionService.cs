namespace Application.Transactions;

public interface ITransactionService
{
    Task<ApiResult<string>> CreateTransaction(CreateTransactionCommand command);
}