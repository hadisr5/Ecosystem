using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU
{
    
    public class TeacherEvalResultViewModel : CoreBaseViewModel 
    {
        [Display(Name = "شاخص ارزیابی")]
        public int TeacherEvalIndexID { get; set; }
		[Display(Name = "کاربر")]
		public int UserID { get; set; }
		[Display(Name = "نتیجه")]
		public string Result { get; set; }

    }
}
