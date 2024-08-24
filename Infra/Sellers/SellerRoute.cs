namespace Infra.Sellers;

public static class SellerRoute
{
    public static string GetAllSellers = "api/Seller";
    public static string GetSellersByFilter = "api/Seller?";
    public static string CreateSeller = "api/Seller";
    public static string UpdateSeller = "api/Seller";
    public static string GetSellerById = "api/Seller/";
    public static string GetCurrentSeller = "api/Seller/current";
    public static string CreateSellerInventory = "api/Seller/Inventory";
    public static string UpdateSellerInventory = "api/Seller/Inventory";
    public static string GetAllInventories = "api/Seller/Inventory";
    public static string GetInventoryById = "api/Seller/Inventory/";
}