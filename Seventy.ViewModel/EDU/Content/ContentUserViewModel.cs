using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU.Content
{
    public class ContentUserViewModel : CoreBaseViewModel
    {
        [Display(Name = "کاربر")]
        public string UserName { get; set; }

        [Display(Name = "کاربر")]
        public int? UserId { get; set; }
    }
}
