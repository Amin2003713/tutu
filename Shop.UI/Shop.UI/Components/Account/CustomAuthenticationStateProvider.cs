using System.Diagnostics;
using System.Security.Claims;
using Application.User.Auth.CommandAndQueries;
using Application.User.Auth.Interfaces;
using Application.User.Auth.Responses;
using Application.User.Users.Interfaces;
using Application.User.Users.Responses;
using Infra.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Shop.UI.Components.Account;

public class CustomAuthenticationServer : RevalidatingServerAuthenticationStateProvider
{
    private readonly PersistentComponentState state;
    private readonly PersistingComponentStateSubscription subscription;
    private readonly ILocalStorage _storage;
    private readonly IUserService UserService;
    private readonly IUserAuthRepository Auth;
    private Task<AuthenticationState>? authenticationStateTask;
    private HttpContext HttpContext;

    public CustomAuthenticationServer(ILoggerFactory loggerFactory,
        PersistentComponentState persistentComponentState, IUserAuthRepository auth, ILocalStorage storage, HttpContext httpContext, IUserService userService) : base(loggerFactory)
    {
        this.state = persistentComponentState;
        Auth = auth;
        _storage = storage;
        HttpContext = httpContext;
        UserService = userService;
        AuthenticationStateChanged += OnAuthenticationStateChanged;
        subscription = state.RegisterOnPersisting(OnPersistingAsync, RenderMode.InteractiveWebAssembly);
    }

    private async Task OnPersistingAsync()
    {
        if (authenticationStateTask is null)
            throw new UnreachableException($"Authentication state not set in {nameof(OnPersistingAsync)}().");

        var authenticationState = await authenticationStateTask;
        var principal = authenticationState.User;

        if (principal.Identity?.IsAuthenticated == true)
        {
            var userId = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;


            if (userId != null)
            {
                state.PersistAsJson(nameof(UserDto), new UserDto()
                {
                    Id = userId,
                    PhoneNumber = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;,
                    
                    new(ClaimTypes.NameIdentifier, userInfo.Id.ToString()),
                    new(ClaimTypes.Name, userInfo.PhoneNumber),
                    new(ClaimTypes.Surname, userInfo.Family),
                    new(ClaimTypes.GivenName, userInfo.Name),
                    new(ClaimTypes.Email, userInfo.Email ?? "@"),
                    new(ClaimTypes.Gender, userInfo.Gender.ToString()),
                    new(ClaimTypes.UserData, userInfo.AvatarName)  
                });
            }
        }
    }

    private void OnAuthenticationStateChanged(Task<AuthenticationState> task)
    {
        authenticationStateTask = task;
    }

    protected override TimeSpan RevalidationInterval => TimeSpan.FromMinutes(30);

    protected override Task<bool> ValidateAuthenticationStateAsync(AuthenticationState authenticationState,
        CancellationToken cancellationToken)
    {
        var user = authenticationState.User;

        return Task.FromResult(user is not null);
    }


    public async Task<bool> Login(LoginCommand command)
    {
        var result = await Auth.Login(command);
        if (!result.IsSuccess)
            return false;

        await _storage.SetAsync("LoginResponse", result.Data);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(await Claims()));

        return true;
    }


    public async Task<bool> Logout(LoginCommand command)
    {
        var result = await Auth.Logout();
        if (!result!.IsSuccess)
            return false;

        await _storage.DeleteAsync("LoginResponse");

        return true;
    }


    private async Task<ClaimsPrincipal> Claims()
    {

        var userInfoResult = await UserService.GetCurrentUser();
        if (userInfoResult is null || !userInfoResult.IsSuccess || userInfoResult.Data is null)
            return null!;

        await _storage.SetAsync("UserInfo", userInfoResult.Data);

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
        return new ClaimsPrincipal(new ClaimsIdentity(claimIdentityList, "Shop-Auth"));

    }
}