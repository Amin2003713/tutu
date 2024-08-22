using Microsoft.AspNetCore.Http;

namespace Application.Category.Interfaces;

public record AddChildCategoryCommand(long ParentId, string Title, string Slug, IFormFile ImageFile, SeoData SeoData);