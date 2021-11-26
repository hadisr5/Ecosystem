using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU
{
    public class CourseEvaluationResultViewModel : CoreBaseViewModel
    {
        [Display(Name = "شاخص")]
        public int CourseEvalIndexID { get; set; }
		[Display(Name = "کاربر")]
		public int UserID { get; set; }
		[Display(Name = "نتیجه")]
		public string Result { get; set; }

    }
}
