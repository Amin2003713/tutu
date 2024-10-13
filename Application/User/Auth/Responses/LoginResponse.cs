namespace Application.User.Auth.Responses;

public record LoginResponse(string Token, string RefreshToken);