using System.Security.Claims;
using Application.Common;
using Application.User.Auth.CommandAndQueries;
using Application.User.Auth.Interfaces;
using Application.User.Auth.Responses;
using Application.User.Users.Interfaces;
using Domain.Common.Api;
using Domain.User.Auth;
using Infra.Utils;
using Infra.Utils.Constants.Storage;
using Infra.Utils.Constants.User;
using Microsoft.AspNetCore.Components.Authorization;

namespace Infra.User.Auth.Implantations;

public class UserAuthRepository(
    IBaseHttpClient client,
    IUserService userService,
    ILocalStorage _localStorage,
    AuthenticationStateProvider _authenticationStateProvider) : IUserAuthRepository
{
    public async Task<ClaimsPrincipal> CurrentUser()
    {
        var state = await _authenticationStateProvider.GetAuthenticationStateAsync();
        return state.User;
    }

    public async Task<ApiResult<LoginResponse>> Login(LoginCommand model)
    {
        try
        {
            var result = await client.PostAsync<LoginCommand, ApiResult<LoginResponse>>(AuthRouts.Login, model);
            if (!result!.IsSuccess)
                return result;

            var token = result.Data.Token;
            var refreshToken = result.Data.RefreshToken;

            await _localStorage.SetAsync(StorageConstants.Local.AuthToken, token);
            await _localStorage.SetAsync(StorageConstants.Local.RefreshToken, refreshToken);

            await client.SetAuthHeader(result.Data);


            ((ClientStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(
                await Claims(result.Data));
            return result;
        }
        catch (Exception e)
        {
            return null!;
        }
    }

    public async Task<ApiResult> Register(RegisterCommand command)
    {
        var result = await client.PostAsync<RegisterCommand, ApiResult>(AuthRouts.Register, command);
        return result!;
    }

    public async Task<ApiResult?> Logout()
    {
        await _localStorage.DeleteAsync(StorageConstants.Local.AuthToken);
        await _localStorage.DeleteAsync(StorageConstants.Local.RefreshToken);
        await _localStorage.DeleteAsync(StorageConstants.Local.UserImageURL);

        await ((ClientStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();

        await client.SetAuthHeader();
        return new ApiResult
        {
            IsSuccess = true
        };
    }

    public async Task<ApiResult<LoginResponse>> RefreshToken(RefreshTokenCommand command)
    {
        var token = await _localStorage.GetAsync<string>(StorageConstants.Local.AuthToken);
        var refreshToken = await _localStorage.GetAsync<string>(StorageConstants.Local.RefreshToken);


        var result = await client.PostAsync<RefreshTokenCommand, ApiResult<LoginResponse>>(AuthRouts.Refresh, command);

        if (!result.IsSuccess) 
            throw new ApplicationException("Something went wrong during the refresh token action");

        token = result.Data.Token;
        refreshToken = result.Data.RefreshToken;

        await _localStorage.SetAsync(StorageConstants.Local.AuthToken, token);
        await _localStorage.SetAsync(StorageConstants.Local.RefreshToken, refreshToken);
        await client.SetAuthHeader(result.Data);
        return new ApiResult<LoginResponse>
        {
            IsSuccess = true
        };
    }

    // public async Task<string> TryRefreshToken()
    // {
    //     // //check if token exists
    //     // var availableToken = await _localStorage.GetAsync<string>(StorageConstants.Local.RefreshToken);
    //     // if (string.IsNullOrEmpty(availableToken)) return string.Empty;
    //     // var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
    //     // var user = authState.User;
    //     // var exp = user.FindFirst(c => c.Type.Equals("exp"))?.Value;
    //     // var expTime = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(exp));
    //     // var timeUTC = DateTime.UtcNow;
    //     // var diff = expTime - timeUTC;
    //     // if (diff.TotalMinutes <= 1)
    //     //     return await RefreshToken();
    //     // return string.Empty;
    // }

    // public async Task<string> TryForceRefreshToken()
    // {
    //     return await RefreshToken();
    // }

    private async Task<ClaimsPrincipal> Claims(LoginResponse login)
    {
        var userInfoResult = await userService.GetCurrentUser(login);
        if (userInfoResult is null || !userInfoResult.IsSuccess || userInfoResult.Data is null)
            return null!;


        var userInfo = userInfoResult.Data;
        var claimIdentityList = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, userInfo.Id.ToString()),
            new(ClaimTypes.MobilePhone, userInfo.PhoneNumber ?? string.Empty),
            new(ClaimTypes.Surname, userInfo.Family ?? string.Empty),
            new(ClaimTypes.Name, userInfo.Name ?? string.Empty),
            new(ClaimTypes.Email, userInfo.Email ?? "@"),
            new(ClaimTypes.Gender, userInfo.Gender.ToString()),
            new(ClaimTypes.UserData, userInfo.AvatarName ?? string.Empty),
            new(AuthConfig.Token, login.Token),
            new(AuthConfig.RefreshToken, login.RefreshToken)
        };
        claimIdentityList.AddRange(userInfo.Roles.Select(a => new Claim(ClaimTypes.Role, $"{a.RoleId}#{a.RoleTitle}"))
            .ToList());

        await _localStorage.SetAsync(UserConstants.UserLocation, userInfo);


        return new ClaimsPrincipal(new ClaimsIdentity(claimIdentityList, "apiauth"));
    }
}