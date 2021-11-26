using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.Core.Users
{
    [System.Runtime.Serialization.DataContract(IsReference = true)]
    public  class LoginViewModel
    {
        [Display(Name = "موبایل")]
        [Required(ErrorMessage = "*")]
        [RegularExpression("^[0][9][0-9][0-9]{8,8}$", ErrorMessage = "شماره موبایل نامعتبر است")]
        public string Mobile { get; set; }

        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "رمز عبور را وارد نمائید")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
    public class ForgotPasswordCodeViewModel
    {
        [Display(Name = "موبایل")]
        [Required(ErrorMessage = "*")]
        [RegularExpression("^[0][9][0-9][0-9]{8,8}$", ErrorMessage = "شماره موبایل نامعتبر است")]
        public string Mobile { get; set; }
        [Display(Name = "کد تغییر رمز")]
        [Required(ErrorMessage = "کد فعال سازی را وارد نمایید")]
        public string Code { get; set; }

        [Display(Name = "رمز ورود")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[0-9])(?=.*[a-z-A-Z-0-9].*[a-z-A-Z-0-9].*[a-z-A-Z]).{6,}$", ErrorMessage = "رمز ورود باید شامل حروف و کاراکتر و بیشتر از 6 کاراکتر باشد")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*")]
        public string Password { get; set; }


        [Display(Name = "تکرار رمز ورود ")]
        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*")]
        [Compare("Password", ErrorMessage = "تکرار رمز اشتباه است")]
        public string ConfirmedPassword { get; set; }
    }
}
