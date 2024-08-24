namespace Infra.Category;

public static class Users
{
    public static string GetAllUsers = "api/Users";
    public static string CreateUser = "api/Users";
    public static string UpdateUser = "api/Users";
    public static string GetCurrentUser = "api/Users/current";
    public static string UpdateCurrentUser = "api/Users/current";
    public static string GetUserById = "api/Users/{userId}";
    public static string UpdateUserPassword = "api/Users/ChangePassword";
}