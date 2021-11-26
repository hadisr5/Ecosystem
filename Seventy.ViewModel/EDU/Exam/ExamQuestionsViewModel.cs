using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU
{
    
    public class ExamQuestionsViewModel : CoreBaseViewModel 
    {
        [Display(Name = "آزمون")]
        public int ExamID { get; set; }
		[Display(Name = "سوال")]
		public int QuestionID { get; set; }
		[Display(Name = "بارم")]
		public int Barom { get; set; }
        
    }
}
