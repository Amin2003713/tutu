using System.Security.Claims;
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


    public List<Claim> Claims()
    {
        var userInfo = this;
        var claimIdentityList = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, userInfo.Id.ToString()),
            new(ClaimTypes.Name, userInfo.PhoneNumber),
            new(ClaimTypes.Surname, userInfo.Family),
            new(ClaimTypes.GivenName, userInfo.Name),
            new(ClaimTypes.Email, userInfo.Email ?? "@"),
            new(ClaimTypes.Gender, userInfo.Gender.ToString()),
            new(ClaimTypes.UserData, userInfo.AvatarName)
        };
        claimIdentityList.AddRange(userInfo.Roles.Select(a => new Claim(ClaimTypes.Role, $"{a.RoleId}#{a.RoleTitle}"))
            .ToList());
        return claimIdentityList;
    }

    public static UserDto FromPrincipal(ClaimsPrincipal principal)
    {
        if (principal.Identity != null && principal.Identity.IsAuthenticated)
        {
            var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var email = principal.FindFirst(ClaimTypes.Email)?.Value;
            var family = principal.FindFirst(ClaimTypes.Surname)?.Value;
            var name = principal.FindFirst(ClaimTypes.GivenName)?.Value;
            var gender = principal.FindFirst(ClaimTypes.Gender)?.Value;
            var avatarName = principal.FindFirst(ClaimTypes.UserData)?.Value;
            var phoneNumber =
                principal.FindFirst(ClaimTypes.Name)?.Value; // Assuming phone number is stored in the 'Name' claim
            // You might need to adjust these to map the correct claims.

            // Assuming roles are stored in claims
            var roles = principal.FindAll(ClaimTypes.Role).Select(r => r.Value).Select(a => new UserRoleDto
            {
                RoleId = Convert.ToInt64(a.Split("#").First()),
                RoleTitle = a.Split("#").Last()
            }).ToList();

            // Now you can populate the UserDto
            if (userId != null)
                return new UserDto
                {
                    Id = Convert.ToInt64(userId),
                    Email = email ?? "@", // Use fallback for null values
                    Family = family,
                    Gender = Enum.Parse<Gender>(gender!),
                    Name = name,
                    Roles = roles,
                    AvatarName = avatarName,
                    PhoneNumber = phoneNumber,
                    Password = string.Empty, // Not available from claims
                    CreationDate = DateTime.Now, // Adjust if creation date needs to come from elsewhere
                    IsActive = true // Adjust based on your application logic
                };
        }

        return new UserDto();
    }

    public string FullName() => $"{Name} {Family}";
}