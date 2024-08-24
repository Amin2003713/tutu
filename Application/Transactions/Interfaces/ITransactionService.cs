using Application.Transactions.CommandAndQueries;

namespace Application.Transactions.Interfaces;

public interface ITransactionService
{
    Task<ApiResult?> CreateTransaction(CreateTransactionCommand command);
}