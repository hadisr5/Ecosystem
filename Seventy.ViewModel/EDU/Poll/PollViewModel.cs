using System;
using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU
{
    public class PollViewModel : CoreBaseViewModel 
    {
		[Display(Name = "هفته آموزشی")]
		public int TrainingWeekID { get; set; }
		[Display(Name = "فایل")]
		public int? FileID { get; set; }
		[Display(Name = "بارم")]
		public int Barom { get; set; }
		[Display(Name = "پاسخ صحیح")]
		public string CorrectAnswer { get; set; }
		[Display(Name = "شروع")]
		public DateTime StartDate { get; set; }
		[Display(Name = "خاتمه")]
		public DateTime EndDate { get; set; }
		[Display(Name = "وضعیت")]
		public string Status { get; set; }
    
    }
}
