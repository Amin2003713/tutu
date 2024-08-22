namespace Application.Users.Auth.CommandAndQueries;

public abstract record RegisterCommand(string PhoneNumber , string Password , string ConfirmPassword);