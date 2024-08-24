using Application.ShippingMethods.ShippingMethods;

namespace Application.ShippingMethods;

public interface IShippingMethodService
{
    Task<List<ShippingMethodDto>> GetShippingMethods();
}