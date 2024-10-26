using Application.Common;
using Application.User.UserAddress.CommandAndQueries;
using Application.User.UserAddress.Interfaces;
using Application.User.UserAddress.Responses;
using DNTPersianUtils.Core.IranCities;
using Domain.Common.Api;
using Domain.User.Address;
using Infra.Utils;
using Province = Domain.User.Address.Province;

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
            UserAddressRouts.DeleteUserAddressById.BuildRequestUrl([addressId.ToString()])!);
    }

    public async Task<ApiResult?> SetActiveAddress(long addressId)
    {
        return await client.PutAsync<object, ApiResult>(
            UserAddressRouts.SetActiveUserAddress.BuildRequestUrl([addressId.ToString()])!, null!);
    }

    public async Task<ApiResult<AddressDto>?> GetAddressById(long id)
    {
        return await client.GetAsync<ApiResult<AddressDto>>(
            UserAddressRouts.GetCurrentUserAddress.BuildRequestUrl([id])!);
    }

    public async Task<ApiResult<List<AddressDto>>?> GetCurrentUserAddresses()
    {
        return await client.GetAsync<ApiResult<List<AddressDto>>>(
            UserAddressRouts.GetCurrentUserAddress);
    }


    public List<Province> ListProvince()
    {
        return (from province in Iran.Provinces
            let countiesList = province.Counties.Select(a =>
                new Counties(a.CountyName,
                    a.Districts.Select(w =>
                        new Districts(w.DistrictName,
                            w.Cities.Select(s => new Cities(s.CityName)).ToList()
                        )
                    ).ToList()
                )
            ).ToList()
            select new Province(province.ProvinceName, countiesList)).ToList();
    }

    public List<Cities> ListCity(string provinceName)
    {
        var province = Iran.Provinces.FirstOrDefault(p => p.ProvinceName == provinceName);

        if (province == null)
            return new List<Cities>(); // Return empty list if province not found

        return province.Counties
            .SelectMany(c => c.Districts) // Flatten the districts
            .SelectMany(d => d.Cities) // Flatten the cities
            .Select(city => new Cities(city.CityName)) // Return only the city name
            .ToList();
    }

}

