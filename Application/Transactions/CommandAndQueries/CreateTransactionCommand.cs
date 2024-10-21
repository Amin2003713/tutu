namespace Application.Transactions.CommandAndQueries
{
    using System.Net.Http;

    public class CreateTransactionCommand
    {
        public long OrderId { get; set; }
        public string SuccessCallBackUrl { get; set; }
        public string ErrorCallBackUrl { get; set; }

        // Method to convert to MultipartFormDataContent
        public MultipartFormDataContent ToMultipartFormData()
        {
            var formData = new MultipartFormDataContent();

            formData.Add(new StringContent(OrderId.ToString()), nameof(OrderId));
            formData.Add(new StringContent(SuccessCallBackUrl), nameof(SuccessCallBackUrl));
            formData.Add(new StringContent(ErrorCallBackUrl), nameof(ErrorCallBackUrl));

            return formData;
        }
    }
}