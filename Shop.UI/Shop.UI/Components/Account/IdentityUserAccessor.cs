using Application.User.Users.Interfaces;
using Application.User.Users.Responses;
using Microsoft.AspNetCore.Identity;


namespace Shop.UI.Components.Account;

internal sealed class IdentityUserAccessor(
    IdentityRedirectManager redirectManager , IUserService userService)
{
    public async Task<UserDto> RequiredUserAsync(HttpContext context)
    {
        var user = await userService.GetCurrentUser();

        if (user is null || user.Data is not null)
        {
            redirectManager.RedirectToWithStatus("Account/InvalidUser",
                $"Error: Unable to load user with ID .", context);
        }

        return user.Data!;
    }
}