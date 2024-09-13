using System.Security.Claims;
using Application.User.Auth.Responses;
using Application.User.Users.Interfaces;
using Infra.Utils;
using Microsoft.AspNetCore.Components.Authorization;

namespace Shop.UI.Components.Account;

public class CustomAuthenticationStateProvider(ILocalStorage storage, IUserService userService) : AuthenticationStateProvider
{
    private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var tokenResult = await storage.GetAsync<LoginResponse>("LoginResponse");

            if (tokenResult is null)
                return await Task.FromResult(new AuthenticationState(_anonymous));


            var userInfoResult =await userService.GetCurrentUser();
            if(userInfoResult is null ||!userInfoResult.IsSuccess || userInfoResult.Data is null )
                return await Task.FromResult(new AuthenticationState(_anonymous));

            await storage.SetAsync("UserInfo" , userInfoResult.Data);

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
            claimIdentityList.AddRange(userInfo.Roles.Select(a => new Claim(ClaimTypes.Role, a.RoleTitle)).ToList());
            var claims = new ClaimsPrincipal(new ClaimsIdentity(claimIdentityList, "CustomAuth"));


            

            return await Task.FromResult(new AuthenticationState(claims));
        }
        catch 
        {

            return await Task.FromResult(new AuthenticationState(_anonymous));
        }
    }

    public async Task NotifyAuthenticationStateChanged()
    {
        NotifyAuthenticationStateChanged(Task.FromResult(await GetAuthenticationStateAsync()));
    }

  
}