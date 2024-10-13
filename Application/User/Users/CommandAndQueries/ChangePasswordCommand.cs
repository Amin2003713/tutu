using System.ComponentModel.DataAnnotations;

namespace Application.User.Users.CommandAndQueries;

public class ChangePasswordCommand
{
    [Required(ErrorMessage = "رمز عبور فعلی ضروری است")]
    public string CurrentPassword { get; set; }

    [Required(ErrorMessage = "رمز عبور جدید ضروری است")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "رمز عبور باید حداقل ۶ کاراکتر و حداکثر ۱۰۰ کاراکتر باشد")]
    public string Password { get; set; }

    [Required(ErrorMessage = "تأیید رمز عبور ضروری است")]
    [Compare("Password", ErrorMessage = "رمز عبور و تأیید رمز عبور مطابقت ندارند")]
    public string ConfirmPassword { get; set; }
}