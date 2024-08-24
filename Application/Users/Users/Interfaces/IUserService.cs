using Application.Users.Users.Users;
using Application.Users.Users.Users.Commands;

namespace Application.Users.Users;

public interface IUserService
{
    Task<ApiResult> CreateUser(CreateUserCommand command);
    Task<ApiResult> EditUser(EditUserCommand command);
    Task<ApiResult> EditUserCurrent(EditUserCommand command);
    Task<ApiResult> ChangePassword(ChangePasswordCommand command);

    Task<UserDto?> GetUserById(long userId);
    Task<UserDto?> GetCurrentUser();
    Task<UserFilterResult> GetUsersByFilter(UserFilterParams filterParams);
}