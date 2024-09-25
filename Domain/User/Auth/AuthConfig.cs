namespace Domain.User.Auth;

public static class AuthConfig
{
    public static string ShopSchema = nameof(ShopSchema);
    public static string AuthCookie = nameof(AuthCookie) + nameof(ShopSchema);
}