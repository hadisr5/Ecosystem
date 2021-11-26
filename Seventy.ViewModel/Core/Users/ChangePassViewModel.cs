using System;
using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.Core.Users
{
    public  class ChangePassword
    {
        [Display(Name = "رمز ورود قدیمی")]
        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*")]
        public String OldPassword { get; set; }

        [Display(Name = "رمز ورود")]
        [DataType(DataType.Password)]
        //[RegularExpression(@"^(?=.*[0-9])(?=.*[a-z-A-Z-0-9].*[a-z-A-Z-0-9].*[a-z-A-Z]).{6,}$", ErrorMessage = "رمز ورود باید شامل حروف و کاراکتر و بیشتر از 6 کاراکتر باشد")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*")]
        public String Password { get; set; }


        [Display(Name = "تکرار رمز ورود ")]
        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*")]
        //[Compare("Password", ErrorMessage = "تکرار رمز اشتباه است")]
        public String ConfirmedPassword { get; set; }
    }
}
