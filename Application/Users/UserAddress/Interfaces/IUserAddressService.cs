using Application.Users.UserAddress.UserAddress;

namespace Application.Users.UserAddress;

public interface IUserAddressService
{
    Task<ApiResult> CreateAddress(CreateUserAddressCommand command);
    Task<ApiResult> EditAddress(EditUserAddressCommand command);
    Task<ApiResult> DeleteAddress(long addressId);
    Task<ApiResult> SetActiveAddress(long addressId);

    Task<AddressDto?> GetAddressById(long id);
    Task<List<AddressDto>> GetUserAddresses();
}