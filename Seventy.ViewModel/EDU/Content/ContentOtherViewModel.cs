using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU.Content
{
    public class ContentOtherViewModel : CoreBaseViewModel
    {
        [Display(Name = "دوره")]
        public int? CourseId { get; set; }

        [Display(Name = "استاد")]
        public int TeacherId { get; set; }

        [Display(Name = "استاد")]
        public string TeacherName { get; set; }

        [Display(Name = "دوره")]
        public string CourseName { get; set; }

        [Display(Name = "شرح دوره")]
        public string CourseDescription { get; set; }
    }
}
