using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU
{
    
    public class UserLessonViewModel : CoreBaseViewModel 
    {
		[Display(Name = "کاربر")]
		public int UserID { get; set; }
		[Display(Name = "درس")]
		public int LessonID { get; set; }
		[Display(Name = "وضعیت")]
		public string Status { get; set; }
		[Display(Name = "میزان رضایت")]
		public int? LikeRank { get; set; }

    }
}
