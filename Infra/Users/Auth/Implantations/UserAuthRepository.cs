using Application.Users.Auth.CommandAndQueries;
using Application.Users.Auth.Interfaces;
using Application.Users.Auth.Responses;
using Domain.Common;
using Domain.Utils;
using Infra.Common;

namespace Infra.Users.Auth.Implantations;

public class UserAuthRepository(BaseHttpClient client)   : IUserAuthRepository
{
    public async Task<ApiResult<LoginResponse>> Login(LoginCommand command)
    {
        var result = await client.PostAsync<LoginCommand, ApiResult<LoginResponse>>( AuthRouts.Login , command);

        return result!;
    }

    public async Task<ApiResult> Register(RegisterCommand command)
    {
        var result = await client.PostAsync<RegisterCommand, ApiResult>(AuthRouts.Register, command);

        return result!;
    }

    public async Task<ApiResult<LoginResponse>> RefreshToken(RefreshTokenCommand command)
    {
        
        var result = await client.PostAsync<RefreshTokenCommand, ApiResult<LoginResponse>>(AuthRouts.Refresh.BuildRequestUrl<Dictionary<string, string>>(
                [new Dictionary<string, string> { { "refreshToken", $"{command.RefreshToken}" } }])!, command);
                                       
        return result!;
    }

    public async Task<ApiResult?> Logout()
    {
        return await client.DeleteAsync<ApiResult>(AuthRouts.Logout);
    }
}