using System.Security.Claims;
using Application.User.Auth.CommandAndQueries;
using Application.User.Auth.Interfaces;
using Application.User.Users.Responses;
using Infra.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Shop.UI.Client.Common.Auth;

public class PersistentClientAuthenticationStateProvider : AuthenticationStateProvider
{

    private static readonly Task<AuthenticationState> DefaultUnauthenticatedTask =
        Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));

    private readonly Task<AuthenticationState> _authenticationStateTask = DefaultUnauthenticatedTask;

    public PersistentClientAuthenticationStateProvider(PersistentComponentState state)
    {
        if (!state.TryTakeFromJson<UserDto>(nameof(UserDto), out var userInfo) || userInfo is null)
        {
            return;
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
        claimIdentityList.AddRange(userInfo.Roles.Select(a => new Claim(ClaimTypes.Role, $"{a.RoleId}#{a.RoleTitle}"))
            .ToList());

        var claimsPrincipal =  new ClaimsPrincipal(new ClaimsIdentity(claimIdentityList,
            authenticationType: nameof(PersistentClientAuthenticationStateProvider)));

        _authenticationStateTask = Task.FromResult(
            new AuthenticationState(claimsPrincipal));
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        return _authenticationStateTask;
    }

}