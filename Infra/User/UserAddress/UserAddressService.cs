using System.Net.Http.Json;
using Application.Common;
using Application.User.UserAddress.CommandAndQueries;
using Application.User.UserAddress.Interfaces;
using Application.User.UserAddress.Responses;
using Domain.Common.Api;
using Infra.Utils;

namespace Infra.User.UserAddress;

public class UserAddressService(IBaseHttpClient client) : IUserAddressService
{
    public async Task<ApiResult?> CreateAddress(CreateUserAddressCommand command)
    {
        return await client.PostAsync<CreateUserAddressCommand, ApiResult>(
            UserAddressRouts.CreateUserAddress, command);
    }

    public async Task<ApiResult?> EditAddress(EditUserAddressCommand command)
    {
        return await client.PutAsync<EditUserAddressCommand, ApiResult>(
            UserAddressRouts.UpdateUserAddress, command);
    }

    public async Task<ApiResult?> DeleteAddress(long addressId)
    {
        return await client.DeleteAsync<ApiResult>(
            UserAddressRouts.DeleteUserAddressById.BuildRequestUrl([addressId])!);
    }

    public async Task<ApiResult?> SetActiveAddress(long addressId)
    {
        return (await client.PutAsync<object, ApiResult>(
            UserAddressRouts.SetActiveUserAddress.BuildRequestUrl([addressId])! , null!));
    }

    public async Task<ApiResult<AddressDto>?> GetAddressById(long id)
    {
        return await client.GetAsync<ApiResult<AddressDto>>(
            UserAddressRouts.GetUserAddressById.BuildRequestUrl([id])!);
    }

    public async Task<ApiResult<List<AddressDto>>?> GetUserAddresses()
    {
        return await client.GetAsync<ApiResult<List<AddressDto>>>(
            UserAddressRouts.GetUserAddressById);
    }
}