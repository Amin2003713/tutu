using System.Diagnostics;
using System.Security.Claims;
using Application.User.Auth.CommandAndQueries;
using Application.User.Auth.Interfaces;
using Application.User.Auth.Responses;
using Application.User.Users.Responses;
using Domain.User.Users;
using Infra.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Shop.UI.Components.Account;

public class CustomServerAuthenticationStateProvider : RevalidatingServerAuthenticationStateProvider
{
    private readonly PersistentComponentState _state;
    private readonly PersistingComponentStateSubscription subscription;
    private Task<AuthenticationState>? authenticationStateTask;
    private IHttpContextAccessor HttpContext;
    





    public CustomServerAuthenticationStateProvider(ILoggerFactory loggerFactory,
        PersistentComponentState persistentComponentState,
         IHttpContextAccessor httpContext) : base(loggerFactory)
    {
        _state = persistentComponentState;
        HttpContext = httpContext;
        AuthenticationStateChanged += OnAuthenticationStateChanged;
        subscription = _state.RegisterOnPersisting(OnPersistingAsync, RenderMode.InteractiveWebAssembly);
    }

    private async Task OnPersistingAsync()
    {
        if (authenticationStateTask is null)
        {
            throw new UnreachableException($"Authentication state not set in {nameof(OnPersistingAsync)}().");
        }

        var authenticationState = await authenticationStateTask;
        var principal = authenticationState.User;

        if (principal.Identity != null && principal.Identity.IsAuthenticated)
        {
            var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var email = principal.FindFirst(ClaimTypes.Email)?.Value;
            var family = principal.FindFirst(ClaimTypes.Surname)?.Value;
            var name = principal.FindFirst(ClaimTypes.GivenName)?.Value;
            var gender = principal.FindFirst(ClaimTypes.Gender)?.Value;
            var avatarName = principal.FindFirst(ClaimTypes.UserData)?.Value;
            var phoneNumber =
                principal.FindFirst(ClaimTypes.Name)?.Value; // Assuming phone number is stored in the 'Name' claim
            // You might need to adjust these to map the correct claims.

            // Assuming roles are stored in claims
            var roles = principal.FindAll(ClaimTypes.Role).Select(r => r.Value).Select(a=> new UserRoleDto()
            {
                RoleId = Convert.ToInt64(a.Split("#").First()),
                RoleTitle= a.Split("#").Last(),
            }).ToList();

            // Now you can populate the UserDto
            if (userId != null)
            {
                _state.PersistAsJson(nameof(UserDto), new UserDto
                {
                    Id = Convert.ToInt64(userId),
                    Email = email ?? "@", // Use fallback for null values
                    Family = family,
                    Gender = Enum.Parse<Gender>(gender!),
                    Name = name,
                    Roles = roles,
                    AvatarName = avatarName,
                    PhoneNumber = phoneNumber,
                    Password = string.Empty, // Not available from claims
                    CreationDate = DateTime.Now, // Adjust if creation date needs to come from elsewhere
                    IsActive = true, // Adjust based on your application logic
                });
            }
        }
    }

    public async Task UpdateAuthenticationStateAsync(ClaimsPrincipal claims)
    {
        var claimsPrincipal = claims ?? new ClaimsPrincipal();

        await HttpContext.HttpContext!.SignInAsync("Shop-Auth", claims!); 

        NotifyAuthenticationStateChanged(
            Task.FromResult(new AuthenticationState(claimsPrincipal)));
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
        if (user is null)
        {
            return Task.FromResult(false);
        }
        else
        {
            return Task.FromResult(true);
            ;
        }

    }

  
}