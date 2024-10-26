using System.Reflection.Metadata;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using Domain.User.Users;

namespace Application.User.Users.Responses;

public class UserDto : BaseDto
{
    public string Name { get; set; }
    public string Family { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string AvatarName { get; set; }
    public bool IsActive { get; set; }
    public Gender Gender { get; set; }
    public List<UserRoleDto> Roles { get; set; }


    public string FullName() => $"{Name} {Family}";

    public string AvatarUrl(string address) =>   $"{address}/images/users/avatar/{AvatarName}";
}