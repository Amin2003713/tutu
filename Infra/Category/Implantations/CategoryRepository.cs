using Application.Category.Interfaces;
using Application.Common;
using Domain.Common;
using Domain.Utils;

namespace Infra.Category.Implantations;

public class CategoryRepository(IBaseHttpClient client) : ICategoryRepository
{
    public async Task<ApiResult<long>?> AddChild(AddChildCategoryCommand command)
    {
        return await client.PostMultipartAsync<AddChildCategoryCommand, ApiResult<long>>
            (CategoryRouts.PostCategoryChildMultipartData, command);
    }

    public async Task<ApiResult?> Edit(EditCategoryCommand command)
    {
        return await client.PutMultipartAsync<EditCategoryCommand, ApiResult>
            (CategoryRouts.PutCategoryMultipartData, command);
    }

    public async Task<ApiResult<long>?> Create(CreateCategoryCommand command)
    {
        return await client.PostMultipartAsync<CreateCategoryCommand, ApiResult<long>>
            (CategoryRouts.PostCategoryMultipartData, command);
    }

    public async Task<ApiResult?> Remove(CategoryIdCommand command)
    {
        return await client.DeleteAsync<ApiResult>
            (CategoryRouts.DeleteCategoryById.BuildRequestUrl([command.Id])!);
    }

    public async Task<ApiResult<CategoryDto>?> GetCategoryById(CategoryIdCommand command)
    {
        return await client.DeleteAsync<ApiResult<CategoryDto>>
            (CategoryRouts.GetCategoryById.BuildRequestUrl([command.Id])!);
    }

    public async Task<ApiResult<List<ChildCategoryDto>>?> GetCategoriesByParentId(CategoryIdCommand command)
    {
        return await client.DeleteAsync<ApiResult<List<ChildCategoryDto>>>
            (CategoryRouts.GetChildListByCategoryId.BuildRequestUrl([command.Id])!);
    }

    public async Task<ApiResult<List<CategoryDto>>?> GetCategories()
    {
        return await client.DeleteAsync<ApiResult<List<CategoryDto>>>
            (CategoryRouts.GetCategoriesList!);
    }
}