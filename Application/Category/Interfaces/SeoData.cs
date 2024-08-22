namespace Application.Category.Interfaces;

public class SeoData
{

    public string? MetaTitle { get; set; }
    public string? MetaDescription { get; set; }
    public string? MetaKeyWords { get; set; }
    public bool IndexPage { get; set; } = true;
    public string? Canonical { get; set; }
}