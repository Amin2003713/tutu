using Microsoft.AspNetCore.Http;

namespace Application.Category.Interfaces;

public record CreateCategoryCommand(string Title, string Slug, IFormFile ImageFile, SeoData SeoData);