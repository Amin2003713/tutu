using System.Security.Claims;
using Application.User.Users.Responses;
using Domain.User.Users;
using Infra.Utils;
using Infra.Utils.Constants.User;

namespace MyShop.Ui.Extensions;

public class UserInfo(ILocalStorage localStorage)
{
    public async Task<string> Email()
    {
        return (await localStorage.GetAsync<UserDto>(UserConstants.UserLocation))?.Email ?? string.Empty;
    }

    public async  Task<string> FirstName()
    {
        return (await localStorage.GetAsync<UserDto>(UserConstants.UserLocation))?.Name ?? string.Empty;
    }

    public async  Task<string> FullName()
    {
        return (await localStorage.GetAsync<UserDto>(UserConstants.UserLocation))?.FullName() ?? string.Empty;

    }

    public async  Task<string> LastName()
    {

                return (await localStorage.GetAsync<UserDto>(UserConstants.UserLocation))?.Family ?? string.Empty;

    }

    public async  Task<string> PhoneNumber()
    {
                return (await localStorage.GetAsync<UserDto>(UserConstants.UserLocation))?.PhoneNumber ?? string.Empty;

    }

    public async  Task<long> UserId()
    {
                return (await localStorage.GetAsync<UserDto>(UserConstants.UserLocation))!.Id;

    }
    public async  Task<string> Avatar()
    {
                return (await localStorage.GetAsync<UserDto>(UserConstants.UserLocation))?.AvatarName ?? string.Empty;

    }

    public async Task<Gender?> Gender()
    {
                return (await localStorage.GetAsync<UserDto>(UserConstants.UserLocation))?.Gender;

    }
}