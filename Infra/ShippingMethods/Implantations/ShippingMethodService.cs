using Application.Common;
using Application.ShippingMethods.Interfaces;
using Application.ShippingMethods.Responses;
using Domain.Common.Api;

namespace Infra.ShippingMethods.Implantations;

public class ShippingMethodService(IBaseHttpClient client) : IShippingMethodService
{

    public async Task<ApiResult<ShippingMethodDto?>?> GetShippingMethods()
    {
        return await client.GetAsync<ApiResult<ShippingMethodDto?>>(
            ShippingMethodRouts.GetAllShippingMethods);
    }
}