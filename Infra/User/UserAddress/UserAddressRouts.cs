namespace Infra.User.UserAddress;

public static class UserAddressRouts
{
    public static string GetAllUserAddresses = "api/UserAddress";
    public static string CreateUserAddress = "api/UserAddress";
    public static string UpdateUserAddress = "api/UserAddress";
    public static string GetUserAddressById = "api/UserAddress/{id}";
    public static string DeleteUserAddressById = "api/UserAddress/{addressId}";
    public static string SetActiveUserAddress = "api/UserAddress/SetActiveAddress/{addressId}";
}