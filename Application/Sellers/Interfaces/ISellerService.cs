using Application.Sellers.CommandAndQueries;
using Application.Sellers.Responses;
using Domain.Sellers;

namespace Application.Sellers.Interfaces;

public interface ISellerService
{
    Task<ApiResult?> CreateSeller(CreateSellerCommand command);
    Task<ApiResult?> EditSeller(EditSellerCommand command);
    Task<ApiResult?> AddInventory(AddSellerInventoryCommand command);
    Task<ApiResult?> EditInventory(EditSellerInventoryCommand command);

    Task<ApiResult<SellerDto>?> GetSellerById(long sellerId);
    Task<ApiResult<SellerDto>?> GetCurrentSeller();
    Task<ApiResult<SellerFilterResult>?> GetSellersByFilter(SellerFilterParams filterParams);

    Task<ApiResult<InventoryDto?>?> GetInventoryById(long inventoryId);
    Task<ApiResult<List<InventoryDto>?>?> GetSellerInventories();
}