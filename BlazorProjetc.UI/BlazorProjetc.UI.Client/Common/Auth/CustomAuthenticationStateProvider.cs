using System.Net.Http.Headers;
using System.Security.Claims;
using Application.User.Auth.Responses;
using Application.User.Users.Responses;
using BlazorProjetc.UI.Client.Common.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorProjetc.UI.Client.Common.Auth;

public class CustomAuthenticationStateProvider(WebAssemblyLocalStorage storage, HttpClient context ,
    NavigationManager _navigationManager)
    : AuthenticationStateProvider
{
    private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            if (_navigationManager.Uri.StartsWith("http"))
            {
                var tokenResult = await storage.GetAsync<LoginResponse>("LoginResponse");

                if (tokenResult is null)
                    return await Task.FromResult(new AuthenticationState(_anonymous));

                var userInfo = await storage.GetAsync<UserDto>("UserInfo");

                if (userInfo is null)
                    return await Task.FromResult(new AuthenticationState(_anonymous));

                var claimIdentityList = new List<Claim>
                {
                    new(ClaimTypes.NameIdentifier, userInfo.Id.ToString()),
                    new(ClaimTypes.Name, userInfo.PhoneNumber),
                    new(ClaimTypes.Surname, userInfo.Family),
                    new(ClaimTypes.GivenName, userInfo.Name),
                    new(ClaimTypes.Email, userInfo.Email),
                    new(ClaimTypes.Gender, userInfo.Gender.ToString()),
                    new(ClaimTypes.UserData, userInfo.AvatarName)
                };
                claimIdentityList.AddRange(userInfo.Roles.Select(a => new Claim(ClaimTypes.Role, a.RoleTitle))
                    .ToList());
                var claims = new ClaimsPrincipal(new ClaimsIdentity(claimIdentityList, "CustomAuth"));


                await AddAuthorizationHeader();


                return await Task.FromResult(new AuthenticationState(claims));
            }

            return await Task.FromResult(new AuthenticationState(_anonymous));

        }
        catch
        {
            await RemoveAuthorizationHeader();

            return await Task.FromResult(new AuthenticationState(_anonymous));
        }
    }

    public async Task UpdateAuthenticationStateAsync(UserDto userDto)
    {
        ClaimsPrincipal claimsPrincipal;

        if (userDto is not null)
        {
            await storage.SetAsync("UserInfo", userDto);

            var claimIdentityList = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, userDto.Id.ToString()),
                new(ClaimTypes.Name, userDto.PhoneNumber),
                new(ClaimTypes.Surname, userDto.Family),
                new(ClaimTypes.GivenName, userDto.Name),
                new(ClaimTypes.Email, userDto.Email),
                new(ClaimTypes.Gender, userDto.Gender.ToString()),
                new(ClaimTypes.UserData, userDto.AvatarName)
            };
            claimIdentityList.AddRange(userDto.Roles.Select(a => new Claim(ClaimTypes.Role, a.RoleTitle)).ToList());
            claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claimIdentityList, "CustomAuth"));
            await AddAuthorizationHeader();
        }
        else
        {
            await storage.DeleteAsync("UserInfo");
            claimsPrincipal = _anonymous;
            await RemoveAuthorizationHeader();
        }

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
    }

    private async Task AddAuthorizationHeader()
    {
        if (_navigationManager.Uri.StartsWith("http"))
        {
            var tokenResult = await storage.GetAsync<LoginResponse>("LoginResponse");

            if (!string.IsNullOrEmpty(tokenResult.Token))
                context.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", tokenResult.Token);
        }
    }

    private async Task RemoveAuthorizationHeader()
    {
        if (_navigationManager.Uri.StartsWith("http"))
        {
            var tokenResult = await storage.GetAsync<LoginResponse>("LoginResponse");

            if (!string.IsNullOrEmpty(tokenResult.Token))
                context.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "");
        }
    }

}