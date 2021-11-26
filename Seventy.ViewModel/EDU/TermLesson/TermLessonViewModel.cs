using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU.TermLesson
{
   public class TermLessonViewModel : CoreBaseViewModel
    {

        [Display(Name = " دوره آموزشی")]
        public string CourseName { get; set; }

        [Display(Name = " گروه")]
        public string GroupName { get; set; }

        [Display(Name = "ترم")]
        public string TermName { get; set; }

        [Display(Name = "درس")]
        public string LessonName { get; set; }

        [Display(Name = "استاد")]
        public string TeacherName { get; set; }

    }
}
