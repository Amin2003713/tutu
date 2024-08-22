using Application.Category.Interfaces;
using Application.Users.Auth.CommandAndQueries;
using Application.Users.Auth.Interfaces;
using Application.Users.Auth.Responses;
using Domain.Common;
using Domain.Utils;
using Infra.Common;
using Infra.Users.Auth;

namespace Infra.Category.Implantations;

public class CategoryRepository(BaseHttpClient client)   : ICategoryRepository
{
    public Task<ApiResult<long>> AddChild(AddChildCategoryCommand command)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResult> Edit(EditCategoryCommand command)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResult<long>> Create(CreateCategoryCommand command)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResult> Remove(long categoryId)
    {
        throw new NotImplementedException();
    }

    public Task<CategoryDto> GetCategoryById(long id)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResult<List<ChildCategoryDto>>> GetCategoriesByParentId(long parentId)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResult<List<CategoryDto>>> GetCategories()
    {
        throw new NotImplementedException();
    }
}