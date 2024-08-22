using Microsoft.AspNetCore.Http;

namespace Application.Category.Interfaces;

public record EditCategoryCommand(long Id, string Title, string Slug, IFormFile? ImageFile, SeoData SeoData);