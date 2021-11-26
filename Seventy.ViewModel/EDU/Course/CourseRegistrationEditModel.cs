using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU.Course
{

    public class CourseRegistrationEditModel : CoreBaseViewModel
    {
        [Required]
        [Display(Name = "فراگیر")]
        public int UserID { get; set; }

        [Required]
        [Display(Name = "گروه آموزشی")]
        public int CourseGroupID { get; set; }

        [Required]
        [Display(Name = " دوره آموزشی")]
        public int CourseID { get; set; }

        [Required]
        [Display(Name = "ترم")]
        public int TermID { get; set; }

        [Display(Name = "وضعیت مدرک")]
        public string DocumentsState { get; set; }

        [Display(Name = "درخواست گواهینامه")]
        public string CertificateType { get; set; }

        [Display(Name = "پیشرفت")]
        public int? Progress { get; set; }
        
        [Display(Name = "میزان رضایت")]
        public int? LikeRank { get; set; }

        [Display(Name = "وضعیت دستاوردها")]
        public string AchievementsState { get; set; }

        [Display(Name = "نوع حضور دوره")]
        public string HozoriState { get; set; }

        [Display(Name = "پکیج پذیرایی")]
        public int? CateringPackId { get; set; }

        [Display(Name = "وضعیت رسید")]
        public string ResidState { get; set; }

    }
}
