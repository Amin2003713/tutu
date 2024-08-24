using System.Net.Http.Json;
using Application.Transactions;
using Domain.Common.Api;

namespace Infra.Transactions;

public class TransactionService : ITransactionService
{
    private readonly HttpClient _client;
    private const string ModuleName = "transaction";
    public TransactionService(HttpClient client)
    {
        _client = client;
    }

    public async Task<ApiResult<string>> CreateTransaction(CreateTransactionCommand command)
    {
        var result = await _client.PostAsJsonAsync(ModuleName, command);
        return await result.Content.ReadFromJsonAsync<ApiResult<string>>();
    }
}