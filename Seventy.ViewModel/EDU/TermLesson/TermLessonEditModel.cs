using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU.TermLesson
{
    public class TermLessonEditModel : CoreBaseViewModel
    {
        [Display(Name = "دوره")]
        public int CourseID { get; set; }

        [Display(Name = "گروه")]
        public int CourseGroupID { get; set; }

        [Display(Name = "ترم")]
        public int TermID { get; set; }

        [Display(Name = "درس")]
        public int LessonID { get; set; }

        [Display(Name = "استاد")]
        public int TeacherID { get; set; }
    }
}
