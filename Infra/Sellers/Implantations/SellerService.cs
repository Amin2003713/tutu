using System.Net.Http.Json;
using Application.Sellers;
using Application.Sellers.Sellers;
using Application.Sellers.Sellers.Commands;
using Domain.Common.Api;

namespace Infra.Sellers;

public class SellerService : ISellerService
{
    private readonly HttpClient _client;
    private const string ModuleName = "seller";
    public SellerService(HttpClient client)
    {
        _client = client;
    }

    public async Task<ApiResult> CreateSeller(CreateSellerCommand command)
    {
        var result = await _client.PostAsJsonAsync(ModuleName, command);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> EditSeller(EditSellerCommand command)
    {
        var result = await _client.PutAsJsonAsync(ModuleName, command);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> AddInventory(AddSellerInventoryCommand command)
    {
        var result = await _client.PostAsJsonAsync($"{ModuleName}/inventory", command);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> EditInventory(EditSellerInventoryCommand command)
    {
        var result = await _client.PutAsJsonAsync($"{ModuleName}/inventory", command);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<SellerDto?> GetSellerById(long sellerId)
    {
        var result = await _client.GetFromJsonAsync<ApiResult<SellerDto?>>($"{ModuleName}/{sellerId}");
        return result.Data;
    }

    public async Task<SellerDto?> GetCurrentSeller()
    {
        var result = await _client.GetFromJsonAsync<ApiResult<SellerDto?>>($"{ModuleName}/current");
        return result.Data;
    }

    public async Task<SellerFilterResult> GetSellersByFilter(SellerFilterParams filterParams)
    {
        var url = filterParams.GenerateBaseFilterUrl(ModuleName) +
                  $"&NationalCode={filterParams.NationalCode}&ShopName={filterParams.ShopName}";

        var result = await _client.GetFromJsonAsync<ApiResult<SellerFilterResult>>(url);
        return result.Data;
    }

    public async Task<InventoryDto?> GetInventoryById(long inventoryId)
    {
        var result = await _client.GetFromJsonAsync<ApiResult<InventoryDto?>>($"{ModuleName}/inventory/{inventoryId}");
        return result.Data;
    }

    public async Task<List<InventoryDto>> GetSellerInventories()
    {
        var result = await _client.GetFromJsonAsync<ApiResult<List<InventoryDto>>>($"{ModuleName}/inventory");
        return result.Data;
    }
}