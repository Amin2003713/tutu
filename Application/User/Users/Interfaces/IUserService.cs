using Application.User.Auth.Responses;
using Application.User.Users.CommandAndQueries;
using Application.User.Users.Responses;
using Domain.User.Users;

namespace Application.User.Users.Interfaces;

public interface IUserService
{
    Task<ApiResult?> CreateUser(CreateUserCommand command);
    Task<ApiResult?> EditUser(EditUserCommand command);
    Task<ApiResult?> EditUserCurrent(EditUserCommand command);
    Task<ApiResult?> ChangePassword(ChangePasswordCommand command);

    Task<ApiResult<UserDto>?> GetUserById(long userId);
    Task<ApiResult<UserDto>?> GetCurrentUser(LoginResponse login = default!);
    Task<ApiResult<UserDto>?> GetUsersByFilter(UserFilterParams filterParams);
}