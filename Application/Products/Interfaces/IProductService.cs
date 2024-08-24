using Application.Products.CommandAndQueries;
using Application.Products.Responses;
using ProductFilterResult = Application.Products.Responses.ProductFilterResult;

namespace Application.Products.Interfaces;

public interface IProductService
{
    Task<ApiResult?> CreateProduct(CreateProductCommand command);
    Task<ApiResult?> EditProduct(EditProductCommand command);
    Task<ApiResult?> AddImage(AddProductImageCommand command);
    Task<ApiResult?> DeleteProductImage(DeleteProductImageCommand command);

    Task<ApiResult<ProductDto?>?> GetProductById(long productId);
    Task<ApiResult<ProductDto?>?> GetProductBySlug(string slug);
    Task<ApiResult<ProductDto?>?> GetSingleProduct(string slug);
    Task<ApiResult<ProductFilterResult>?> GetProductByFilter(ProductFilterParams filterParams);
    Task<ApiResult<ProductShopResult>?> GetProductForShop(ProductShopFilterParam filterParams);

}