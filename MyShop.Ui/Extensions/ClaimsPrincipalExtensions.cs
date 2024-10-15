using System.Security.Claims;

namespace MyShop.Ui.Extensions;

internal static class ClaimsPrincipalExtensions
{
    internal static string GetEmail(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirst(ClaimTypes.Email)!.Value;
    }

    internal static string FirstName(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirst(ClaimTypes.Name)!.Value;
    }

    internal static string FullName(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirst(ClaimTypes.GivenName)!.Value +" "+
               claimsPrincipal.FindFirst(ClaimTypes.Surname)!.Value;
    }

    internal static string GetLastName(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirst(ClaimTypes.Surname)!.Value;
    }

    internal static string GetPhoneNumber(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirst(ClaimTypes.MobilePhone)!.Value;
    }

    internal static string GetUserId(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)!.Value;
    }
}