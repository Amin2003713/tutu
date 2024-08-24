namespace Infra.Products;

public static class ProductRouts
{
    public static string GetAllProducts = "api/Product";
    public static string GetAllProductsFiltered = "api/Product?";
    public static string CreateProduct = "api/Product";
    public static string UpdateProduct = "api/Product";
    public static string GetProductShop = "api/Product/Shop?";
    public static string GetProductById = "api/Product/";
    public static string GetProductBySlug = "api/Product/bySlug/";
    public static string GetSingleProductBySlug = "api/Product/single/";
    public static string UploadProductImages = "api/Product/images";
    public static string DeleteProductImages = "api/Product/images";
}