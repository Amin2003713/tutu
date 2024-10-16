using System.Security.Claims;
using Application.User.Users.Responses;

namespace MyShop.Ui.Extensions;

internal static class ClaimsPrincipalExtensions
{
    internal static string Email(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirst(ClaimTypes.Email)!.Value;
    }

    internal static string FirstName(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirst(ClaimTypes.Name)!.Value;
    }

    internal static string FullName(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirst(ClaimTypes.Name)!.Value +" "+
               claimsPrincipal.FindFirst(ClaimTypes.Surname)!.Value;
    }

    internal static string LastName(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirst(ClaimTypes.Surname)!.Value;
    }

    internal static string PhoneNumber(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirst(ClaimTypes.MobilePhone)!.Value;
    }

    internal static string UserId(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)!.Value;
    }
    internal static string Avatar(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirst(ClaimTypes.UserData)!.Value;
    }

    internal static string Gender(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirst(ClaimTypes.Gender)!.Value;
    }
}