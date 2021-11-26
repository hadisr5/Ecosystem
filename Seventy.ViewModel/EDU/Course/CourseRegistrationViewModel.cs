using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU
{
    
    public class CourseRegistrationViewModel : CoreBaseViewModel 
    {
		[Display(Name = "کاربر")]
		public int UserID { get; set; }
		[Display(Name = "دوره")]
		public int CourseID { get; set; }
		[Display(Name = "ترم")]
		public int TermID { get; set; }
		[Display(Name = "وضعیت مدارک")]
		public string DocumentsState { get; set; }
		[Display(Name = "نوع گواهینامه")]
		public string CertificateType { get; set; }
		[Display(Name = "پیشرفت")]
		public int? Progress { get; set; }
		[Display(Name = "میزان رضایت")]
		public int? LikeRank { get; set; }
		[Display(Name = "وضعیت دستاوردها")]
		public string AchievementsState { get; set; }
		[Display(Name = "نوع دوره ")]
		public string HozoriState { get; set; }
		[Display(Name = "بسته پذیرایی")]
		public int? CateringPackID { get; set; }
		[Display(Name = "وضعیت رسید")]
		public string ResidState { get; set; }
		[Display(Name = " گروه آموزشی")]
		[Required]
		public int CourseGroupID { get; set; }
		[Display(Name = "درخواست گواهینامه")]
		public string CertificateID { get; set; }

	}
}
