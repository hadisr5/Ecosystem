using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU
{
    public class TeacherLikeViewModel : CoreBaseViewModel 
    {
		[Display(Name = "کاربر")]
		public int UserID { get; set; }
		[Display(Name = "استاد")]
		public int TeacherID { get; set; }
		[Display(Name = "دوره")]
		public int CourseID { get; set; }
		[Display(Name = "میزان رضایت")]
		public int? LikeRank { get; set; }

    }
}
