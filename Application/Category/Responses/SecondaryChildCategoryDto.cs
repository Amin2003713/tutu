namespace Application.Category.Responses;

public class SecondaryChildCategoryDto : BaseDto
{
    public string Title { get; set; }
    public string Slug { get; set; }
    public string ImageName { get; set; }
    public SeoData SeoData { get; set; }
    public long ParentId { get; set; }
}