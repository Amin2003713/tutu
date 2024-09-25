using System.Security.Claims;
using Application.User.Users.Responses;
using Domain.User.Auth;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;

namespace MyShop.Ui.Client.AuthenticationProvider;

public class ClientAuthStateProvider : AuthenticationStateProvider
{
    public IHttpContextAccessor  Accessor { get; set; }
    private readonly Task<AuthenticationState> _authenticationStateTask =
        Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));

    public ClientAuthStateProvider(PersistentComponentState componentState)
    {
        ClaimsPrincipal principal;
        if (componentState.TryTakeFromJson<UserDto>(nameof(UserDto), out var loggedInUser) &&
            loggedInUser is not null && loggedInUser.Id > 0)
        {
            
            var claims = loggedInUser.Claims();
            var identity = new ClaimsIdentity(claims, AuthConfig.ShopSchema);
            principal = new ClaimsPrincipal(identity);
        }
        else
        {
            var identity = new ClaimsIdentity();
            principal = new ClaimsPrincipal(identity);
        }

        var authState = new AuthenticationState(principal);
        _authenticationStateTask = Task.FromResult(authState);
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync() =>
        _authenticationStateTask;
}