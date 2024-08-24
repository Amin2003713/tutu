namespace Application.User.Auth.CommandAndQueries;

public abstract record RegisterCommand(string PhoneNumber , string Password , string ConfirmPassword);