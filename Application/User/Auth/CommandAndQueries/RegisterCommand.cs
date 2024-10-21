using System.ComponentModel.DataAnnotations;

namespace Application.User.Auth.CommandAndQueries;

public class RegisterCommand
{
    [Required(ErrorMessage = "وارد کردن شماره تلفن ضروری است.")]
    [RegularExpression(@"^(\+98|0)?9\d{9}$", ErrorMessage = "شماره تلفن وارد شده معتبر نیست.")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "وارد کردن رمز عبور ضروری است.")]
    [StringLength(100, ErrorMessage = "رمز عبور باید حداقل {2} و حداکثر {1} کاراکتر باشد.", MinimumLength = 6)]
    public string Password { get; set; }

    [Required(ErrorMessage = "پذیرش قوانین و مقررات ضروری است.")]
    [Range(typeof(bool), "true", "true", ErrorMessage = "باید قوانین و مقررات را بپذیرید.")]
    public bool AcceptedTerms { get; set; }
}
