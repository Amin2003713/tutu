using Domain.Common;

namespace Application.Category.Interfaces;

public interface ICategoryRepository
{
    Task<ApiResult<long>?> AddChild(AddChildCategoryCommand command);
    Task<ApiResult?> Edit(EditCategoryCommand command);
    Task<ApiResult<long>?> Create(CreateCategoryCommand command);
    Task<ApiResult?> Remove(CategoryIdCommand command);
    Task<ApiResult<CategoryDto>?> GetCategoryById(CategoryIdCommand command);
    Task<ApiResult<List<ChildCategoryDto>>?> GetCategoriesByParentId(CategoryIdCommand command);
    Task<ApiResult<List<CategoryDto>>?> GetCategories();
}