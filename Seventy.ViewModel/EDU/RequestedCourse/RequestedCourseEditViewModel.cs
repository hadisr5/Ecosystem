using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU.RequestedCourse
{
    public class RequestedCourseEditViewModel : CoreBaseViewModel
    {
        [Display(Name = "نوع")]
        public string CourseType { get; set; }

        [Display(Name = "عنوان")]
        public string Title { get; set; }

        [Display(Name = "وضعیت")]
        public string Status { get; set; }
    }
}
