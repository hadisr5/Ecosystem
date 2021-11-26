using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU
{
    public class TeacherEvalIndexViewModel : CoreBaseViewModel 
    {
        [Display(Name = "استاد")]
        public int TeacherID { get; set; }
		[Display(Name = "دسته بندی")]
		public string Category { get; set; }

    }
}
