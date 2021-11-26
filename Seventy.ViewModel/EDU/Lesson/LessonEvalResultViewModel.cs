using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU
{
    public class LessonEvalResultViewModel : CoreBaseViewModel 
    {
        [Display(Name = "شاخص ارزیابی")]
        public int LessonEvalIndexID { get; set; }
		[Display(Name = "کاربر")]
		public int UserID { get; set; }
		[Display(Name = "نتیجه")]
		public string Result { get; set; }
       
    }
}
