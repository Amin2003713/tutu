using System.Net.Http;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Text.Json;

public class CreateProductCommand
{
    public string Title { get; set; }
    public IFormFile ImageFile { get; set; }
    public string Description { get; set; }
    public long CategoryId { get; set; }
    public long SubCategoryId { get; set; }
    public long? SecondarySubCategoryId { get; set; }
    public string Slug { get; set; }
    public SeoData SeoData { get; set; }
    public Dictionary<string, string> Specifications { get; set; }

    public MultipartFormDataContent ToMultipartFormData()
    {
        var formData = new MultipartFormDataContent();

        formData.Add(new StringContent(Title), nameof(Title));
        formData.Add(new StringContent(Description), nameof(Description));
        formData.Add(new StringContent(CategoryId.ToString()), nameof(CategoryId));
        formData.Add(new StringContent(SubCategoryId.ToString()), nameof(SubCategoryId));
        formData.Add(new StringContent(SecondarySubCategoryId?.ToString() ?? string.Empty), nameof(SecondarySubCategoryId));
        formData.Add(new StringContent(Slug), nameof(Slug));

        if (ImageFile != null)
        {
            var streamContent = new StreamContent(ImageFile.OpenReadStream());
            streamContent.Headers.ContentType = new MediaTypeHeaderValue(ImageFile.ContentType);
            formData.Add(streamContent, nameof(ImageFile), ImageFile.FileName);
        }

        if (SeoData != null)
        {
            var seoDataJson = JsonSerializer.Serialize(SeoData);
            formData.Add(new StringContent(seoDataJson, System.Text.Encoding.UTF8, "application/json"), nameof(SeoData));
        }

        // Serialize Specifications dictionary as JSON
        if (Specifications != null)
        {
            var specsJson = JsonSerializer.Serialize(Specifications);
            formData.Add(new StringContent(specsJson, System.Text.Encoding.UTF8, "application/json"), nameof(Specifications));
        }

        return formData;
    }
}
