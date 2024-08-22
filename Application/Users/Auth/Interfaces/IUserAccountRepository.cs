using Application.Users.Auth.CommandAndQueries;
using Application.Users.Auth.Responses;
using Domain.Common;

namespace Application.Users.Auth.Interfaces;

public interface IUserAuthRepository
{
   Task<ApiResult<LoginResponse>> Login(LoginCommand command);
   Task<ApiResult> Register(RegisterCommand command);
   Task<ApiResult<LoginResponse>> RefreshToken(RefreshTokenCommand command);
   Task<ApiResult?> Logout();
}