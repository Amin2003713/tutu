namespace Application.Products.CommandAndQueries
{
    using System.Net.Http;
    using Microsoft.AspNetCore.Http;
    using System.Net.Http.Headers;

    public class AddProductImageCommand
    {
        public IFormFile ImageFile { get; set; }
        public long ProductId { get; set; }
        public int Sequence { get; set; }

        // Method to convert to MultipartFormDataContent
        public MultipartFormDataContent ToMultipartFormData()
        {
            var formData = new MultipartFormDataContent();

            formData.Add(new StringContent(ProductId.ToString()), nameof(ProductId));
            formData.Add(new StringContent(Sequence.ToString()), nameof(Sequence));

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