using Application.Common;
using Application.Sellers.CommandAndQueries;
using Application.Sellers.Interfaces;
using Application.Sellers.Responses;
using Domain.Common.Api;
using Domain.Sellers;
using Infra.Utils;

namespace Infra.Sellers.Implantations;

public class SellerService(IBaseHttpClient client) : ISellerService
{
    public async Task<ApiResult?> CreateSeller(CreateSellerCommand command)
    {
        return await client.PostAsync<CreateSellerCommand, ApiResult>(SellerRoute.CreateSeller, command);
    }

    public async Task<ApiResult?> EditSeller(EditSellerCommand command)
    {
        return await client.PutAsync<EditSellerCommand, ApiResult>(SellerRoute.UpdateSeller, command);
    }

    public async Task<ApiResult?> AddInventory(AddSellerInventoryCommand command)
    {
        return await client.PostAsync<AddSellerInventoryCommand, ApiResult>(SellerRoute.CreateSellerInventory, command);
    }

    public async Task<ApiResult?> EditInventory(EditSellerInventoryCommand command)
    {
        return await client.PutAsync<EditSellerInventoryCommand, ApiResult>(SellerRoute.UpdateSellerInventory, command);
    }

    public async Task<ApiResult<SellerDto>?> GetSellerById(long sellerId)
    {
        return await client.GetAsync<ApiResult<SellerDto>>(SellerRoute.GetSellerById.BuildRequestUrl([sellerId])!);
    }

    public async Task<ApiResult<SellerDto>?> GetCurrentSeller()
    {
        return await client.GetAsync<ApiResult<SellerDto>>(SellerRoute.GetCurrentSeller);
    }

    public async Task<ApiResult<SellerFilterResult>?> GetSellersByFilter(SellerFilterParams filterParams)
    {
        return await client.GetAsync<ApiResult<SellerFilterResult>>(
            SellerRoute.GetSellersByFilter.ToQueryString(filterParams));
    }

    public async Task<ApiResult<InventoryDto?>?> GetInventoryById(long inventoryId)
    {
        return await client.GetAsync<ApiResult<InventoryDto?>>(
            SellerRoute.GetInventoryById.BuildRequestUrl([inventoryId])!);
    }

    public async Task<ApiResult<List<InventoryDto>?>?> GetSellerInventories()
    {
        return await client.GetAsync<ApiResult<List<InventoryDto>?>>(
            SellerRoute.GetAllInventories);
    }
}