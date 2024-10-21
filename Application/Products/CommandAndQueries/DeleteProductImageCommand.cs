namespace Application.Products.CommandAndQueries
{
    using System.Net.Http;

    public class DeleteProductImageCommand
    {
        public long ImageId { get; set; }
        public long ProductId { get; set; }

        // Method to convert to MultipartFormDataContent
        public MultipartFormDataContent ToMultipartFormData()
        {
            var formData = new MultipartFormDataContent();

            formData.Add(new StringContent(ImageId.ToString()), nameof(ImageId));
            formData.Add(new StringContent(ProductId.ToString()), nameof(ProductId));

            return formData;
        }
    }
}