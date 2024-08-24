using Application.Products.CommandAndQueries;
using ProductFilterResult = Application.Products.Responses.ProductFilterResult;

namespace Application.Products.Interfaces;

public interface IProductService
{
    Task<ApiResult> CreateProduct(CreateProductCommand command);
    Task<ApiResult> EditProduct(EditProductCommand command);
    Task<ApiResult> AddImage(AddProductImageCommand command);
    Task<ApiResult> DeleteProductImage(DeleteProductImageCommand command);

    Task<ProductDto?>GetProductById(long productId);
    Task<ProductDto?> GetProductBySlug(string slug);
    Task<SingleProductDto?> GetSingleProduct(string slug);
    Task<ProductFilterResult> GetProductByFilter(ProductFilterParams filterParams);
    Task<ProductShopResult> GetProductForShop(ProductShopFilterParam filterParams);

}