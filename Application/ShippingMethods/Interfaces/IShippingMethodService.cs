using Application.ShippingMethods.Responses;

namespace Application.ShippingMethods.Interfaces;

public interface IShippingMethodService
{
    Task<ApiResult<ShippingMethodDto?>?> GetShippingMethods();
}