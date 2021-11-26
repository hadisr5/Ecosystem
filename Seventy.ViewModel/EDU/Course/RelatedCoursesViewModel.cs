using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU
{
    
    public class RelatedCoursesViewModel : CoreBaseViewModel 
    {
        [Display(Name = "دوره")]
        public string  FirstCourseTitle { get; set; }
		[Display(Name = "دوره مرتبط")]
		public string SecondCourseTitle { get; set; }

    }
}
