namespace Application.Category.CommandAndQueries;

public record AddChildCategoryCommand(long ParentId, string Title, string Slug, IFormFile ImageFile, SeoData SeoData);