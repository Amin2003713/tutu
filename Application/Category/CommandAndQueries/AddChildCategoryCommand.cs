using System.Net.Http;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Text.Json;

public class AddChildCategoryCommand
{
    public long ParentId { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
    public IFormFile ImageFile { get; set; }
    public SeoData SeoData { get; set; }


    // Method to convert to MultipartFormDataContent
    public MultipartFormDataContent ToMultipartFormData()
    {
        var formData = new MultipartFormDataContent();

        formData.Add(new StringContent(ParentId.ToString()), nameof(ParentId));
        formData.Add(new StringContent(Title), nameof(Title));
        formData.Add(new StringContent(Slug), nameof(Slug));

        // Handling the IFormFile (ImageFile)
        if (ImageFile != null)
        {
            var streamContent = new StreamContent(ImageFile.OpenReadStream());
            streamContent.Headers.ContentType = new MediaTypeHeaderValue(ImageFile.ContentType);
            formData.Add(streamContent, nameof(ImageFile), ImageFile.FileName);
        }

        // Handling the SeoData (serialize it to JSON)
        if (SeoData != null)
        {
            var seoDataJson = JsonSerializer.Serialize(SeoData);
            formData.Add(new StringContent(seoDataJson, System.Text.Encoding.UTF8, "application/json"),
                nameof(SeoData));
        }

        return formData;
    }
}