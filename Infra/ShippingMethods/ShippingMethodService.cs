using System.Net.Http.Json;
using Application.ShippingMethods;
using Application.ShippingMethods.ShippingMethods;
using Domain.Common.Api;

namespace Infra.ShippingMethods;

public class ShippingMethodService : IShippingMethodService
{
    private readonly HttpClient _client;
    private const string ModuleName = "ShippingMethod";
    public ShippingMethodService(HttpClient client)
    {
        _client = client;
    }
    public async Task<List<ShippingMethodDto>> GetShippingMethods()
    {
        var result = await _client.GetFromJsonAsync<ApiResult<List<ShippingMethodDto>>>($"{ModuleName}");
        return result?.Data ?? new();
    }
}