using Application.Common;
using Application.User.Users.CommandAndQueries;
using Application.User.Users.Interfaces;
using Application.User.Users.Responses;
using Domain.Common.Api;
using Domain.User.Users;
using Infra.Utils;

namespace Infra.User.Users.Implantations;

public class UserService(IBaseHttpClient client) : IUserService
{

    public async Task<ApiResult?> CreateUser(CreateUserCommand command)
    {
        return await client.PostAsync<CreateUserCommand, ApiResult>(
            UsersRoutes.CreateUser, command);
    }

    public async Task<ApiResult?> EditUser(EditUserCommand command)
    {
        return await client.PutMultipartAsync<EditUserCommand, ApiResult>(
            UsersRoutes.UpdateUser, command);
    }

    public async Task<ApiResult?> EditUserCurrent(EditUserCommand command)
    {
        return await client.PutMultipartAsync<EditUserCommand, ApiResult>(
            UsersRoutes.UpdateCurrentUser, command);
    }

    public async Task<ApiResult?> ChangePassword(ChangePasswordCommand command)
    {
        return await client.PutMultipartAsync<ChangePasswordCommand, ApiResult>(
            UsersRoutes.UpdateUserPassword, command);
    }

    public async Task<ApiResult<UserDto>?> GetUserById(long userId)
    {
        return await client.GetAsync<ApiResult<UserDto>>(
            UsersRoutes.UpdateUser.BuildRequestUrl([userId])!);
    }

    public async Task<ApiResult<UserDto>?> GetCurrentUser()
    {
        return await client.GetAsync<ApiResult<UserDto>>(
            UsersRoutes.UpdateCurrentUser);
    }

    public async Task<ApiResult<UserDto>?> GetUsersByFilter(UserFilterParams filterParams)
    {
        return await client.GetAsync<ApiResult<UserDto>>(
            UsersRoutes.UpdateUser.ToQueryString(filterParams));
    }
}