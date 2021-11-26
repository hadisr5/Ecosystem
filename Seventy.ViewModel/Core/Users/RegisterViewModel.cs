using System;
using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.Core.Users
{
    public class RegisterViewModel: RegisterationViewModel
    {

        [Display(Name = "رمز ورود")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[0-9])(?=.*[a-z-A-Z-0-9].*[a-z-A-Z-0-9].*[a-z-A-Z]).{6,}$", ErrorMessage = "رمز ورود باید شامل حروف و کاراکتر و بیشتر از 6 کاراکتر باشد")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*")]
        public String Password { get; set; }


        [Display(Name = "تکرار رمز ورود ")]
        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*")]
        [Compare("Password", ErrorMessage = "تکرار رمز اشتباه است")]
        public String ConfirmedPassword { get; set; }

        [Display(Name = "کد فعال سازی")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*")]
        public string ActivationCode { get; set; }
    }
    public class ActivationViewModel: RegisterationViewModel
    {
    }
    public class RegisterationViewModel
    {
        [Display(Name = "موبایل")]
        [RegularExpression("^[0][9][0-9][0-9]{8,8}$", ErrorMessage = "شماره موبایل نامعتبر است")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*")]
        public string Mobile { get; set; }

        public string ReturnUrl { get; set; }
    }
}
