using Application.User.Auth.CommandAndQueries;
using Application.User.Auth.Interfaces;
using Application.User.Auth.Responses;
using Domain.Common;
using Domain.Common.Api;
using Infra.Common;
using Infra.User.Auth;
using Infra.Utils;

namespace Infra.User.Auth.Implantations;

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