using Domain.User.Users;
using Microsoft.Net.Http.Headers;
using MediaTypeHeaderValue = System.Net.Http.Headers.MediaTypeHeaderValue;

namespace Application.User.Users.CommandAndQueries;

public class EditUserCommand
{
    public long UserId { get; set; }
    public string Name { get; set; }
    public string Family { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public IFormFile? Avatar { get; set; }
    public Gender Gender { get; set; }



    public MultipartFormDataContent ToMultipartFormData()
    {
        var formData = new MultipartFormDataContent();
        // Add form fields
        formData.Add(new StringContent(UserId.ToString()), "UserId");
        formData.Add(new StringContent(Name), "Name");
        formData.Add(new StringContent(Family), "Family");
        formData.Add(new StringContent(PhoneNumber), "PhoneNumber");
        formData.Add(new StringContent(Email), "Email");
        formData.Add(new StringContent(Gender.ToString()), "Gender");

        if (Avatar != null)
        {
            var fileStream = Avatar.OpenReadStream();
            var fileContent = new StreamContent(fileStream);
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(Avatar.ContentType);

            // Note: `prop.Name` from the IFormFile is sent as "Avatar"
            formData.Add(fileContent, "Avatar", Avatar.FileName);
        }

        return formData;
    }
}