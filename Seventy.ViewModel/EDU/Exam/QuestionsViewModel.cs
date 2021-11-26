using Seventy.DomainClass.EDU;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU
{
    public class QuestionsViewModel : CoreBaseViewModel 
    {
		[Display(Name = "درس")]
		public int LessonID { get; set; }
		[Display(Name ="عنوان درس")]
		public string LessonTitle { get; set; }
		[Display(Name = "سطح سوال")]
		public int QuestionLevel { get; set; }
		[Display(Name = "متن سوال")]
		public string Title { get; set; }
		[Display(Name ="بارم")]
		public int Barom { get; set; } //در برگزاری آزمون استفاده میشه
		[Display(Name = "چند گزینه‌ای")]
		public bool MultiOption { get; set; } = false;
		[Display(Name = "گزینه ها")]
		public List<QuestionOptionsViewModel> AnswerOptions { get; set; }
		[Display(Name ="فایل")]
		public int? FileID { get; set; }
	}
}
