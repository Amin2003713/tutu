namespace Infra;

public static class Product
{
    public static string GetAllProducts = "api/Product";
    public static string CreateProduct = "api/Product";
    public static string UpdateProduct = "api/Product";
    public static string GetProductShop = "api/Product/Shop";
    public static string GetProductById = "api/Product/{productId}";
    public static string GetProductBySlug = "api/Product/bySlug/{slug}";
    public static string GetSingleProductBySlug = "api/Product/single/{slug}";
    public static string UploadProductImages = "api/Product/images";
    public static string DeleteProductImages = "api/Product/images";
}