namespace Application.User.Auth.CommandAndQueries;

public class RegisterCommand
{
    public string PhoneNumber { get; set; } 
    public string Password { get; set; } 
    public string ConfirmPassword { get; set; }

}