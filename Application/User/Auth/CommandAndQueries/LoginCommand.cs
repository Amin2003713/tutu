using System.ComponentModel.DataAnnotations;
using MudBlazor;

namespace Application.User.Auth.CommandAndQueries;

public class LoginCommand
{
    [Required(ErrorMessage = "شماره تلفن الزامی است.")]
    [Phone(ErrorMessage = "لطفاً یک شماره تلفن معتبر وارد کنید.")]
    [Label("شماره همراه")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "رمز عبور الزامی است.")]
    [StringLength(100, ErrorMessage = "رمز عبور باید حداقل {2} و حداکثر {1} کاراکتر باشد.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Label("کلمه عبور")]
    public string Password { get; set; }
}