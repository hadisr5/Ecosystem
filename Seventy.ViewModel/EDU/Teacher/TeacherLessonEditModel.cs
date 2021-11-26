using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU
{
    public class TeacherLessonEditModel : CoreBaseViewModel 
    {
        [Display(Name = "نام مدرس")]
        [Required]

        public int TeacherID { get; set; }
        [Display(Name = "نام درس")]
        [Required]
        public int LessonID { get; set; }


    }
}
