using System.ComponentModel.DataAnnotations;

namespace Application.User.Auth.CommandAndQueries
{
    public class LoginCommand
    {
        [Required(ErrorMessage = "شماره تلفن الزامی است.")]
        [Phone(ErrorMessage = "لطفاً یک شماره تلفن معتبر وارد کنید.")]
        [Display(Name = "شماره تلفن")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "رمز عبور الزامی است.")]
        [StringLength(100, ErrorMessage = "رمز عبور باید حداقل {2} و حداکثر {1} کاراکتر باشد.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }
    }
}