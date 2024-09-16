using System.Security.Claims;
using Application.User.Auth.Responses;
using Application.User.Users.Interfaces;
using Application.User.Users.Responses;
using Infra.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Shop.UI.Client.Common.Auth;

public class CustomAuthenticationStateProvider(
    PersistentComponentState persistentState) : AuthenticationStateProvider
{
    private static readonly Task<AuthenticationState> _unauthenticatedTask =
        Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));

    public override  Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            if (!persistentState.TryTakeFromJson<UserDto>(nameof(UserDto), out var userInfo) || userInfo is null)
            {
                return _unauthenticatedTask;
            }

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
            var claims =  (new ClaimsIdentity(claimIdentityList, "CustomAuth"));




            return Task.FromResult(
                new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claims as IEnumerable<Claim>,
                    authenticationType: nameof(CustomAuthenticationStateProvider)))));
        }
        catch 
        {

            return _unauthenticatedTask;
        }
    }

  
}