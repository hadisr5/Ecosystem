using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU
{
    
    public class RelatedCoursesEditModel : CoreBaseViewModel 
    {
        [Display(Name = "دوره")]
        [Required(ErrorMessage = "دوره را وارد نمایید")]
        public int FirstCourseID { get; set; }
		[Display(Name = "دوره مرتبط")]
        [Required(ErrorMessage ="دوره مرتبط را وارد نمایید")]
		public int SecondCourseID { get; set; }

    }
}
