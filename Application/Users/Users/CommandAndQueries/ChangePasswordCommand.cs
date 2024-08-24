namespace Application.Users.Users.Users.Commands;

public class ChangePasswordCommand
{
    public string CurrentPassword { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}