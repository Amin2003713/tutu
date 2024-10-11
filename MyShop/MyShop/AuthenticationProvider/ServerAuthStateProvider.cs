using Application.User.Users.Responses;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Components.Web;
using MyShop.Components.Account;

namespace MyShop.Ui.AuthenticationProvider;

public class ServerAuthStateProvider : ServerAuthenticationStateProvider, IDisposable
{
    private readonly PersistingComponentStateSubscription _subscription;
    private readonly PersistentComponentState _componentState;
    private readonly IdentityRedirectManager _redirectManager;

    private Task<AuthenticationState> _authenticationStateTask;

    public ServerAuthStateProvider(PersistentComponentState componentState, IdentityRedirectManager redirectManager)
    {
        _componentState = componentState;
        _redirectManager = redirectManager;

        AuthenticationStateChanged += ServerAuthStateProvider_AuthenticationStateChanged;

        _subscription =
            _componentState.RegisterOnPersisting(PersistComponentStateAsync, RenderMode.InteractiveWebAssembly);
    }

    private void ServerAuthStateProvider_AuthenticationStateChanged(Task<AuthenticationState> task) =>
        _authenticationStateTask = task;

    private async Task PersistComponentStateAsync()
    {
        var authState = await _authenticationStateTask;

        if (authState.User.Identity?.IsAuthenticated == true)
        {
            var loggedInUser = UserDto.FromPrincipal(authState.User);

            _componentState.PersistAsJson(nameof(UserDto), loggedInUser);
        } 
    }

    public void Dispose()
    {
        _subscription.Dispose();
        AuthenticationStateChanged -= ServerAuthStateProvider_AuthenticationStateChanged;
    }
}