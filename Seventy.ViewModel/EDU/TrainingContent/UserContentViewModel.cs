using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU
{
    
    public class UserContentViewModel : CoreBaseViewModel 
    {
		[Display(Name = "کاربر")]
		public int UserID { get; set; }
		[Display(Name = "دوره")]
		public int CourseID { get; set; }
		[Display(Name = "وضعیت")]
		public string Status { get; set; }
		[Display(Name = "پیشرفت")]
		public int? Progress { get; set; }
		[Display(Name = "میزان رضایت")]
		public int? LikeRank { get; set; }

    }
}
