using System.Text;
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
        var content = new MultipartFormDataContent();

        content.Add(new StringContent(UserId.ToString()  , Encoding.UTF8), "UserId");
        content.Add(new StringContent(Name, Encoding.UTF8), "Name");
        content.Add(new StringContent(Family , Encoding.UTF8), "Family");
        content.Add(new StringContent(PhoneNumber , Encoding.UTF8), "PhoneNumber");
        content.Add(new StringContent(Email , Encoding.UTF8), "Email");
        content.Add(new StringContent(Gender.ToString() , Encoding.UTF8), "Gender");

        // Add the avatar file if it exists
        if (Avatar != null)
        {
            var fileContent = new StreamContent(Avatar.OpenReadStream())
            {
                Headers =
                {
                    ContentType = MediaTypeHeaderValue.Parse(Avatar.ContentType)
                }
            };
            content.Add(fileContent, "Avatar", Avatar.FileName);
        }

        return (content);
    }
}