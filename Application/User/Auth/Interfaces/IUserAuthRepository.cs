﻿using System.Security.Claims;
using Application.User.Auth.CommandAndQueries;
using Application.User.Auth.Responses;

namespace Application.User.Auth.Interfaces;

public interface IUserAuthRepository
{
    Task<(bool result, string massage, ClaimsPrincipal Token)> Login(LoginCommand command);
    Task<ApiResult> Register(RegisterCommand command);
    Task<ApiResult<LoginResponse>> RefreshToken(RefreshTokenCommand command);
    Task<ApiResult?> Logout();
}