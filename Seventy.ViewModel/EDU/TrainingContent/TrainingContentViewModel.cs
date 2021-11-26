using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU
{
    
    public class TrainingContentViewModel : CoreBaseViewModel 
    {
		[Display(Name = "عنوان محتوی")]
		public string Title { get; set; }

		[Display(Name = "دوره")]
		public int CourseID { get; set; }

		[Display(Name = "نوع محتوی")]
		public string ContentType { get; set; }

		[Display(Name = "وضعیت دمو")]
		public string DemoState { get; set; }

		[Display(Name = "فایل")]
		[Required(ErrorMessage ="فایل محتوی آموزشی انتخاب نشده است")]
		public IFormFile File { get; set; }

		[Display(Name = "دستاورد")]
		public string Achievement { get; set; }
		[Display(Name = "فایل آموزشی")]
		public int? FileID { get; set; }

	}
}
