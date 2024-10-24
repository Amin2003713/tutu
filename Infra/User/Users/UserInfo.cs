using Application.User.Users.Responses;
using Domain.User.Users;
using Infra.Utils;
using Infra.Utils.Constants.User;

namespace Infra.User.Users;

public class UserInfo(ILocalStorage localStorage)
{
    private Task<UserDto?> User { get; set; } = localStorage.GetAsync<UserDto>(UserConstants.UserLocation);

    public async Task<string> FirstName() => (await User)?.Name ?? "";
    public async Task<string> LastName() => (await User)?.Family ?? "";
    public async Task<string> PhoneNumber() => (await User)?.PhoneNumber ?? "";
    public async Task<string> Email() => (await User)?.Email ?? "";
    public async Task<Gender> Gender() => (await User)?.Gender ?? Domain.User.Users.Gender.None;
    public async Task<string> Avatar() => (await User)?.AvatarName ?? "";
    public async Task<long> Id() => (await User)!.Id;
    public string FullName() => $"{FirstName()} {LastName()}".Trim();
}