namespace Application.User.Auth.CommandAndQueries;

public record LoginCommand(string PhoneNumber, string Password);