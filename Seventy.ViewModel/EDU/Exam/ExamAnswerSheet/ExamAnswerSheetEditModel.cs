using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU.Exam.ExamAnswerSheet
{
    public class ExamAnswerSheetEditModel : CoreBaseViewModel 
    {
		[Display(Name ="ردیف")]
		public int RowNumber { get; set; }
		[Display(Name = "آزمون")]
		public int ExamID { get; set; }
		[Display(Name = "کاربر")]
		public int UserID { get; set; }
		[Display(Name = "سوال")]
		public int QuestionID { get; set; }
		[Display(Name ="بارم سوال")]
		public float QuestionBarom { get; set; }
		[Display(Name = "پاسخ")]
		public string Answer { get; set; }
		[Display(Name ="بارم بدست آمده")]
		public double? AchievedBarom { get; set; }

	}
}
