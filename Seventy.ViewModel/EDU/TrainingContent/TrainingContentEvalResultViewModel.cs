using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU
{
    
    public class TrainingContentEvalResultViewModel : CoreBaseViewModel 
    {
        [Display(Name = "شاخص ارزیابی")]
        public int ContentEvalIndexID { get; set; }
		[Display(Name = "کاربر")]
		public int UserID { get; set; }
		[Display(Name = "نتیجه")]
		public string Result { get; set; }

    }
}
