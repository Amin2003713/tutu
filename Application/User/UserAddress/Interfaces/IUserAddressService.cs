using Application.User.UserAddress.CommandAndQueries;
using Application.User.UserAddress.Responses;

namespace Application.User.UserAddress.Interfaces;

public interface IUserAddressService
{
    Task<ApiResult?> CreateAddress(CreateUserAddressCommand command);
    Task<ApiResult?> EditAddress(EditUserAddressCommand command);
    Task<ApiResult?> DeleteAddress(long addressId);
    Task<ApiResult?> SetActiveAddress(long addressId);

    Task<ApiResult<AddressDto>?> GetAddressById(long id);
    Task<ApiResult<List<AddressDto>>?> GetCurrentUserAddresses();
}