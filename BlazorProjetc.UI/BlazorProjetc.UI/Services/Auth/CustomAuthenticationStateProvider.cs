using System.Net.Http.Headers;
using System.Security.Claims;
using Application.User.Auth.Responses;
using Application.User.Users.Responses;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorProjetc.UI.Services.Auth;

public class CustomAuthenticationStateProvider(
    LocalStorage storage,
    HttpClient context,
    NavigationManager navigationManager) : AuthenticationStateProvider
{
    private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            if ((navigationManager.Uri.StartsWith("http://") || navigationManager.Uri.StartsWith("https://")))
                return await Task.FromResult(new AuthenticationState(_anonymous));

            var tokenResult = await storage.GetAsync<LoginResponse>("LoginResponse");

            if (!tokenResult.Success)
                return await Task.FromResult(new AuthenticationState(_anonymous));

            var userInfo = await storage.GetAsync<UserDto>("UserInfo");

            if (!userInfo.Success)
                return await Task.FromResult(new AuthenticationState(_anonymous));

            var claimIdentityList = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, userInfo.Value.Id.ToString()),
                new(ClaimTypes.Name, userInfo.Value.PhoneNumber),
                new(ClaimTypes.Surname, userInfo.Value.Family),
                new(ClaimTypes.GivenName, userInfo.Value.Name),
                new(ClaimTypes.Email, userInfo.Value.Email),
                new(ClaimTypes.Gender, userInfo.Value.Gender.ToString()),
                new(ClaimTypes.UserData, userInfo.Value.AvatarName)
            };
            claimIdentityList.AddRange(userInfo.Value.Roles.Select(a => new Claim(ClaimTypes.Role, a.RoleTitle))
                .ToList());
            var claims = new ClaimsPrincipal(new ClaimsIdentity(claimIdentityList, "CustomAuth"));


             await AddAuthorizationHeader();


            return await Task.FromResult(new AuthenticationState(claims));
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
        if (!(navigationManager.Uri.StartsWith("http://") || navigationManager.Uri.StartsWith("https://")))
        {
            var tokenResult = await storage.GetAsync<LoginResponse>("LoginResponse");

            if(!tokenResult.Success)
                return;

            if (!string.IsNullOrEmpty(tokenResult.Value!.Token))
                context.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", tokenResult.Value.Token);
        }
    }

    private async Task RemoveAuthorizationHeader()
    {
        if (!(navigationManager.Uri.StartsWith("http://") || navigationManager.Uri.StartsWith("https://")))
        {
            var tokenResult = await storage.GetAsync<LoginResponse>("LoginResponse");

            if (!tokenResult.Success)
                return;

            if (!string.IsNullOrEmpty(tokenResult.Value!.Token))
                context.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "");
        }
    }
}