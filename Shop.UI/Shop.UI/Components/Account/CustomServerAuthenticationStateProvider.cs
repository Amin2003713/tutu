using System.Diagnostics;
using System.Security.Claims;
using Application.User.Auth.CommandAndQueries;
using Application.User.Auth.Interfaces;
using Application.User.Auth.Responses;
using Application.User.Users.Interfaces;
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
    private readonly IUserService _userService;
    private readonly IUserAuthRepository _authRepository;
    private readonly ILocalStorage _storage;
    private IHttpContextAccessor _context;




    public CustomServerAuthenticationStateProvider(ILoggerFactory loggerFactory,
        PersistentComponentState persistentComponentState, ILocalStorage storage, IUserService userService, IUserAuthRepository authRepository,
        IHttpContextAccessor context) : base(loggerFactory)
    {
        _state = persistentComponentState;
        _storage = storage;
        _userService = userService;
        _authRepository = authRepository;
        _context = context;
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


    public async Task<(bool result, string massage)> Login(LoginCommand command)
    {
        try
        {
            var result = await _authRepository.Login(command);
            if (!result.IsSuccess)
                return (false, result.MetaData.Message);

            await _storage.SetAsync("LoginResponse", result.Data);

            var claims = await Claims();
            if (claims is null)
                return (false, "توکن شما معتبر نیست.");

            await _context.HttpContext!.SignInAsync("Shop-Auth", claims!, new AuthenticationProperties() { IsPersistent = true });

            return (true, "شما باموفقیت وارد سایت شدید");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return (true, e.ToString());
        }

    }


    public async Task<(bool result, string massage)> Logout()
    {
        try
        {
            var result = await _authRepository.Logout();
            if (!result.IsSuccess)
                return (false, result.MetaData.Message);

            await _storage.DeleteAsync("LoginResponse");

            await _context.HttpContext!.SignOutAsync("Shop-Auth");

            return (true, "شما باموفقیت وارد سایت شدید");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return (true, e.ToString());
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

    private async Task<ClaimsPrincipal> Claims()
    {
        // var tokenResult = await Local.GetAsync<LoginResponse>("LoginResponse");
        //
        // if (tokenResult.Value is null)
        //     return null!;

        var userInfoResult = await _userService.GetCurrentUser();
        if (userInfoResult is null || !userInfoResult.IsSuccess || userInfoResult.Data is null)
            return null!;


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
        claimIdentityList.AddRange(userInfo.Roles.Select(a => new Claim(ClaimTypes.Role, $"{a.RoleId}#{a.RoleTitle}")).ToList());
        return new ClaimsPrincipal(new ClaimsIdentity(claimIdentityList, "Shop-Auth" ));

    }
}