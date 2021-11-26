using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU
{
    public class LessonEvalIndexViewModel : CoreBaseViewModel
    {
        [Display(Name = "درس")]
        public int LessonID { get; set; }
		[Display(Name = "دسته بندی")]
		public string Category { get; set; }
        
    }
}
