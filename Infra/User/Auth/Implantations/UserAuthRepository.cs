using System.Security.Claims;
using Application.Common;
using Application.User.Auth.CommandAndQueries;
using Application.User.Auth.Interfaces;
using Application.User.Auth.Responses;
using Application.User.Users.Interfaces;
using Domain.Common.Api;
using Infra.Common;
using Infra.Utils;
using Microsoft.AspNetCore.Http;

namespace Infra.User.Auth.Implantations;

public class UserAuthRepository(IBaseHttpClient client , IUserService userService , ILocalStorage storage) : IUserAuthRepository
{
    public async Task<(bool result, string massage, ClaimsPrincipal Token)> Login(LoginCommand command)
    {
        var result = await client.PostAsync<LoginCommand, ApiResult<LoginResponse>>(AuthRouts.Login, command);

        if (!result!.IsSuccess)
            return (false, result.MetaData.Message , null!);

        await storage.SetAsync("LoginResponse", result.Data);

        var claims = await Claims();
        if (claims is null)
            return (false, "توکن شما معتبر نیست." , null!);

        return (true, "شما باموفقیت وارد سایت شدید" , claims);
    }

    public async Task<ApiResult> Register(RegisterCommand command)
    {
        var result = await client.PostAsync<RegisterCommand, ApiResult>(AuthRouts.Register, command);

        return result!;
    }

    public async Task<ApiResult<LoginResponse>> RefreshToken(RefreshTokenCommand command)
    {
        var result = await client.PostAsync<RefreshTokenCommand, ApiResult<LoginResponse>>(
            AuthRouts.Refresh.BuildRequestUrl<Dictionary<string, string>>(
                [new Dictionary<string, string> { { "refreshToken", $"{command.RefreshToken}" } }])!, command);

        return result!;
    }

    public async Task<ApiResult?> Logout()
    {
        return await client.DeleteAsync<ApiResult>(AuthRouts.Logout);
    }

    private async Task<ClaimsPrincipal> Claims()
    {
        var userInfoResult = await userService.GetCurrentUser();
        if (userInfoResult is null || !userInfoResult.IsSuccess || userInfoResult.Data is null)
            return null!;


        var userInfo = userInfoResult.Data;
        var claimIdentityList = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, userInfo.Id.ToString()),
            new(ClaimTypes.Name, userInfo.PhoneNumber),
            new(ClaimTypes.Surname, userInfo.Family),
            new(ClaimTypes.GivenName, userInfo.Name),
            new(ClaimTypes.Email, userInfo.Email ?? "@"),
            new(ClaimTypes.Gender, userInfo.Gender.ToString()),
            new(ClaimTypes.UserData, userInfo.AvatarName)
        };
        claimIdentityList.AddRange(userInfo.Roles.Select(a => new Claim(ClaimTypes.Role, $"{a.RoleId}#{a.RoleTitle}"))
            .ToList());
        return new ClaimsPrincipal(new ClaimsIdentity(claimIdentityList, "Shop-Auth"));

    }

}