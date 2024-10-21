using Application.Common;
using Application.Transactions.CommandAndQueries;
using Application.Transactions.Interfaces;
using Domain.Common.Api;

namespace Infra.Transactions.Implantations;

public class TransactionService(IBaseHttpClient client) : ITransactionService
{
    public async Task<ApiResult?> CreateTransaction(CreateTransactionCommand command)
    {
        return await client.PostMultipartAsync<CreateTransactionCommand, ApiResult>(
            TransactionRouts.CreateTransaction, command.ToMultipartFormData());
    }
}