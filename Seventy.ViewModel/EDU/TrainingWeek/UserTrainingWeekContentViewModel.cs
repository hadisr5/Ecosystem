using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU
{
    
    public class UserTrainingWeekContentViewModel : CoreBaseViewModel 
    {
		public int TermID { get; set; }
		public int CourseID { get; set; }
		public int CourseGroupID { get; set; }
		public int LessonID { get; set; }
		[Display(Name = "هفته آموزشی")]
		public int TrainingWeekID { get; set; }
		public int ContentID { get; set; }
		public string ContentType { get; set; }
		public string ContentTitle { get; set; }

		[Display(Name = "کاربر")]
		public int UserID { get; set; }
		[Display(Name = "پیشرفت")]
		public int? Progress { get; set; }
		[Display(Name = "نتیجه")]
		public bool Result { get; set; }
		[Display(Name = "میزان رضایت")]
		public int? LikeRank { get; set; }

    }
}
