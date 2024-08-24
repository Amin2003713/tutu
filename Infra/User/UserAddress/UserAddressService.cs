using System.Net.Http.Json;
using Application.Users.UserAddress;
using Application.Users.UserAddress.UserAddress;
using Domain.Common.Api;

namespace Infra.User.UserAddress;

public class UserAddressService : IUserAddressService
{
    private readonly HttpClient _client;
    private const string ModuleName = "UserAddress";
    public UserAddressService(HttpClient client)
    {
        _client = client;
    }
    public async Task<ApiResult> CreateAddress(CreateUserAddressCommand command)
    {
        var result = await _client.PostAsJsonAsync(ModuleName, command);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> EditAddress(EditUserAddressCommand command)
    {
        var result = await _client.PutAsJsonAsync(ModuleName, command);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> DeleteAddress(long addressId)
    {
        var result = await _client.DeleteAsync($"{ModuleName}/{addressId}");
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> SetActiveAddress(long addressId)
    {
        var result = await _client.PutAsync($"{ModuleName}/SetActiveAddress/{addressId}", null);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<AddressDto?> GetAddressById(long id)
    {
        var result = await _client.GetFromJsonAsync<ApiResult<AddressDto?>>($"{ModuleName}/{id}");
        return result.Data;
    }

    public async Task<List<AddressDto>> GetUserAddresses()
    {
        var result = await _client.GetFromJsonAsync<ApiResult<List<AddressDto>?>>(ModuleName);
        return result.Data;
    }
}