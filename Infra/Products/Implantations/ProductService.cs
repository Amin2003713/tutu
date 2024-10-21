using Application.Common;
using Application.Products.CommandAndQueries;
using Application.Products.Interfaces;
using Application.Products.Responses;
using Domain.Common.Api;
using Domain.Products;
using Infra.Utils;
using ProductFilterResult = Application.Products.Responses.ProductFilterResult;

namespace Infra.Products.Implantations;

public class ProductService(IBaseHttpClient client) : IProductService
{
    public async Task<ApiResult?> CreateProduct(CreateProductCommand command)
    {
        return await client.PostMultipartAsync<CreateProductCommand, ApiResult>(ProductRouts.CreateProduct, command.ToMultipartFormData());
    }

    public async Task<ApiResult?> EditProduct(EditProductCommand command)
    {
        return await client.PutMultipartAsync<EditProductCommand, ApiResult>(ProductRouts.UpdateProduct, command.ToMultipartFormData());
    }

    public async Task<ApiResult?> AddImage(AddProductImageCommand command)
    {
        return await client.PostMultipartAsync<AddProductImageCommand, ApiResult>(ProductRouts.UploadProductImages,
            command.ToMultipartFormData());
    }

    public async Task<ApiResult?> DeleteProductImage(DeleteProductImageCommand command)
    {
        return await client
            .PostMultipartAsync<DeleteProductImageCommand, ApiResult>
                (ProductRouts.DeleteProductImages, command.ToMultipartFormData());
    }

    public async Task<ApiResult<ProductDto?>?> GetProductById(long productId)
    {
        return await client
            .GetAsync<ApiResult<ProductDto?>>
                (ProductRouts.GetProductById.BuildRequestUrl([productId])!);
    }

    public async Task<ApiResult<ProductDto?>?> GetProductBySlug(string slug)
    {
        return await client
            .GetAsync<ApiResult<ProductDto?>>
                (ProductRouts.GetProductBySlug.BuildRequestUrl([slug])!);
    }

    public async Task<ApiResult<ProductDto?>?> GetSingleProduct(string slug)
    {
        return await client
            .GetAsync<ApiResult<ProductDto?>>
                (ProductRouts.GetSingleProductBySlug.BuildRequestUrl([slug])!);
    }

    public async Task<ApiResult<ProductFilterResult>?> GetProductByFilter(ProductFilterParams filterParams)
    {
        var requestParams = new List<Dictionary<string, string>>
        {
            new() { { "pageId", filterParams.PageId.ToString() } },
            new() { { "take", filterParams.Take.ToString() } },
            new() { { "Title", filterParams.Title! } },
            new() { { "Id", filterParams.Id.ToString()! } },
            new() { { "Slug", filterParams.Slug! } }
        };
        return await client.GetAsync<ApiResult<ProductFilterResult>>(
            ProductRouts.GetAllProductsFiltered.ToQueryString(filterParams));
    }

    public async Task<ApiResult<ProductShopResult>?> GetProductForShop(ProductShopFilterParam filterParams)
    {
        return await client.GetAsync<ApiResult<ProductShopResult>>(
            ProductRouts.GetAllProducts.ToQueryString(filterParams)!);
    }
}