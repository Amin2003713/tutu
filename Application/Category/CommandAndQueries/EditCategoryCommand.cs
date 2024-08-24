namespace Application.Category.CommandAndQueries;

public record EditCategoryCommand(long Id, string Title, string Slug, IFormFile? ImageFile, SeoData SeoData);