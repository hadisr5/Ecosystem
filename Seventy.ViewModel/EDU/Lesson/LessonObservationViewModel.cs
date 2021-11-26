using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU
{
    public class LessonObservationViewModel : CoreBaseViewModel 
    {
        [Display(Name = "کاربر")]
        public int UserID { get; set; }
		[Display(Name = "درس")]
		public int LessonID { get; set; }

    }
}
