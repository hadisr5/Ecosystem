using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU
{
    public class FavoriteCoursesViewModel : CoreBaseViewModel 
    {
        [Display(Name = "کاربر")]
        public int UserID { get; set; }
		[Display(Name = "دوره")]
		public int CourseID { get; set; }
		[Display(Name = "میزان رضایت")]
		public int? LikeRank { get; set; }

    }
}
