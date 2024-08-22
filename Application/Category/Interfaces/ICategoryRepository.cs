using Domain.Common;

namespace Application.Category.Interfaces;

public interface ICategoryRepository
{
    Task<ApiResult<long>> AddChild(AddChildCategoryCommand command);
    Task<ApiResult> Edit(EditCategoryCommand command);
    Task<ApiResult<long>> Create(CreateCategoryCommand command);
    Task<ApiResult> Remove(long categoryId);
    Task<CategoryDto> GetCategoryById(long id);
    Task<ApiResult<List<ChildCategoryDto>>> GetCategoriesByParentId(long parentId);
    Task<ApiResult<List<CategoryDto>>> GetCategories();
}