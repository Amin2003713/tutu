using System.ComponentModel.DataAnnotations;

namespace Application.User.Auth.CommandAndQueries
{
    public class LoginCommand
    {
        [Required(ErrorMessage = "لطفاً شماره موبایل خود را وارد کنید")]
        [RegularExpression(@"^(\+98|0)?9\d{9}$", ErrorMessage = "شماره موبایل وارد شده معتبر نیست")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "لطفاً کلمه عبور خود را وارد کنید")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "کلمه عبور باید حداقل ۶ و حداکثر ۱۰۰ کاراکتر باشد")]
        public string Password { get; set; }
    }
}