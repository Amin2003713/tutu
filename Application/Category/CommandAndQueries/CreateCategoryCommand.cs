namespace Application.Category.CommandAndQueries;

public record CreateCategoryCommand(string Title, string Slug, IFormFile ImageFile, SeoData SeoData);