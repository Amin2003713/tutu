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
}