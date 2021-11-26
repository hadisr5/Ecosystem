using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU
{
    public class ExerciseUserViewModel : CoreBaseViewModel
    {
		[Display(Name = "تمرین")]
		public int ExerciseID { get; set; }
		[Display(Name = "کاربر")]
		public int UserID { get; set; }
		[Display(Name = "پاسخ")]
		public string Answer { get; set; }
		[Display(Name = "فایل")]
		public int FileID { get; set; }
		[Display(Name = "نتیجه")]
		public string Result { get; set; }
		[Display(Name = "میزان رضایت")]
		public int? LikeRank { get; set; }
        
    }
}
