namespace Application.Sliders.CommandAndQueries
{
    using System.Net.Http;
    using Microsoft.AspNetCore.Http;
    using System.Net.Http.Headers;

    public class CreateSliderCommand
    {
        public string Link { get; set; }
        public IFormFile ImageFile { get; set; }
        public string Title { get; set; }

        // Method to convert to MultipartFormDataContent
        public MultipartFormDataContent ToMultipartFormData()
        {
            var formData = new MultipartFormDataContent();

            formData.Add(new StringContent(Link), nameof(Link));
            formData.Add(new StringContent(Title), nameof(Title));

            if (ImageFile != null)
            {
                var streamContent = new StreamContent(ImageFile.OpenReadStream());
                streamContent.Headers.ContentType = new MediaTypeHeaderValue(ImageFile.ContentType);
                formData.Add(streamContent, nameof(ImageFile), ImageFile.FileName);
            }

            return formData;
        }
    }
}