using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU
{
    public class RequestForContentViewModel : CoreBaseViewModel 
    {
		[Display(Name = "کاربر")]
		public int UserID { get; set; }
		[Display(Name = "دوره")]
		public int CourseID { get; set; }
		[Display(Name = "عنوان محتوی")]
		public string Title { get; set; }
		[Display(Name = "وضعیت")]
		public string Status { get; set; }

    }
}
