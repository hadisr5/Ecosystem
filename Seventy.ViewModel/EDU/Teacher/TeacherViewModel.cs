using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU
{
    
    public class TeacherViewModel : CoreBaseViewModel 
    {
        [Display(Name = "کاربر")]
        public int UserID { get; set; }

    }
}
