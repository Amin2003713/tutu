namespace Domain.User.Auth;

public static class AuthConfig
{
    public static string ShopSchema = nameof(ShopSchema);
    public static string AuthCookie = nameof(AuthCookie) + nameof(ShopSchema);
    public static string RefreshToken = nameof(RefreshToken) + nameof(ShopSchema);
    public static string Token = nameof(Token) + nameof(ShopSchema);
    public static string UserInfo = nameof(ShopSchema) + nameof(UserInfo);
}