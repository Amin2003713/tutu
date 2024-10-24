using System.ComponentModel.DataAnnotations;

namespace Application.User.UserAddress.CommandAndQueries;

public class CreateUserAddressCommand
{
    [Required(ErrorMessage = "استان الزامی است")]
    public string Shire { get; set; }

    [Required(ErrorMessage = "شهر الزامی است")]
    public string City { get; set; }

    // [Required(ErrorMessage = "کد پستی الزامی است")]
    // [RegularExpression(@"^\d{10}$", ErrorMessage = "کد پستی باید ۱۰ رقم باشد")]
    // public string PostalCode { get; set; }

    [Required(ErrorMessage = "آدرس پستی الزامی است")]
    [MaxLength(200, ErrorMessage = "آدرس پستی نباید بیشتر از ۲۰۰ کاراکتر باشد")]
    public string PostalAddress { get; set; }

    [Required(ErrorMessage = "شماره تلفن الزامی است")]
    [RegularExpression(@"^(\+98|0)?9\d{9}$", ErrorMessage = "شماره تلفن باید معتبر باشد")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "نام الزامی است")]
    [MaxLength(50, ErrorMessage = "نام نباید بیشتر از ۵۰ کاراکتر باشد")]
    public string Name { get; set; }

    [Required(ErrorMessage = "نام خانوادگی الزامی است")]
    [MaxLength(50, ErrorMessage = "نام خانوادگی نباید بیشتر از ۵۰ کاراکتر باشد")]
    public string Family { get; set; }
}