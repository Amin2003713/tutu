namespace Infra.User.Auth;

public static class AuthRouts
{
    public static string Login = "api/Auth/login";
    public static string Register = "api/Auth/register";
    public static string Refresh = "api/Auth/RefreshToken?";
    public static string Logout = "api/Auth/logout";
}