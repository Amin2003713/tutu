using System.Diagnostics;
using System.Security.Claims;
using Application.User.Auth.Responses;
using Application.User.Users.Interfaces;
using Application.User.Users.Responses;
using Infra.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Shop.UI.Components.Account;

public class CustomAuthenticationStateProvider : RevalidatingServerAuthenticationStateProvider
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly PersistentComponentState _state;
    private readonly IdentityOptions _options;
    private readonly IUserService userService;
    private ProtectedLocalStorage Storage;

    private readonly PersistingComponentStateSubscription _subscription;

    private Task<AuthenticationState>? _authenticationStateTask;

    public CustomAuthenticationStateProvider(
        ILoggerFactory loggerFactory,
        IServiceScopeFactory scopeFactory,
        PersistentComponentState state,
        IOptions<IdentityOptions> options,
        IUserService userService, ProtectedLocalStorage storage)
        : base(loggerFactory)
    {
        _scopeFactory = scopeFactory;
        _state = state;
        this.userService = userService;
        Storage = storage;
        _options = options.Value;

        AuthenticationStateChanged += OnAuthenticationStateChanged;
        _subscription = state.RegisterOnPersisting(OnPersistingAsync, RenderMode.InteractiveWebAssembly);
    }

    public async Task OnPersistingAsync()
    {
        try
        {
            if (_authenticationStateTask is null)
            {
                throw new UnreachableException(
                    $"Authentication state not set in {nameof(RevalidatingServerAuthenticationStateProvider)}.{nameof(OnPersistingAsync)}().");
            }

            var authenticationState = await _authenticationStateTask;
            var principal = authenticationState.User;

            if (principal.Identity?.IsAuthenticated == true)
            {

                var userInfoResult = await userService.GetCurrentUser();
                if (userInfoResult is null || !userInfoResult.IsSuccess || userInfoResult.Data is null)
                    throw new UnreachableException(
                        $"Authentication state not set in {nameof(RevalidatingServerAuthenticationStateProvider)}.{nameof(OnPersistingAsync)}().");

                await Storage.SetAsync("UserInfo", userInfoResult.Data);

                var userInfo = userInfoResult.Data;

                if (userInfo is not null)
                {
                    _state.PersistAsJson(nameof(UserDto), userInfo);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

  

    private void OnAuthenticationStateChanged(Task<AuthenticationState> authenticationStateTask)
    {
        _authenticationStateTask = authenticationStateTask;
    }

    protected override async Task<bool> ValidateAuthenticationStateAsync(
        AuthenticationState authenticationState, CancellationToken cancellationToken)
    {
        await using var scope = _scopeFactory.CreateAsyncScope();
        return ValidateSecurityStampAsync(authenticationState.User);
    }

    private bool ValidateSecurityStampAsync(ClaimsPrincipal principal)
    {
        if (principal.Identity?.IsAuthenticated is false)
        {
            return false;
        }

        return true;
    }

    protected override void Dispose(bool disposing)
    {
        _subscription.Dispose();
        AuthenticationStateChanged -= OnAuthenticationStateChanged;
        base.Dispose(disposing);
    }
    protected override TimeSpan RevalidationInterval => TimeSpan.FromMinutes(30);
}