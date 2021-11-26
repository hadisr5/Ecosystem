using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU
{
    public class CourseObservationViewModel : CoreBaseViewModel
    {
        [Display(Name = "کاربر")]
        public int UserID { get; set; }
		[Display(Name = "دوره")]
		public int CourseID { get; set; }

    }
}
