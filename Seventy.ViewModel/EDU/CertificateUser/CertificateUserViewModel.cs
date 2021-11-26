using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU.CertificateUser
{
    public class CertificateUserViewModel : CoreBaseViewModel
    {
        [Display(Name = "گواهینامه")]
        public int CertificateID { get; set; }

        [Display(Name = "کاربر")]
        public int UserID { get; set; }

        [Display(Name = "نمره")]
        public int? Grade { get; set; }

        [Display(Name = "دوره")]
        public int CourseID { get; set; }

        [Display(Name = "گروه")]
        public int CourseGroupID { get; set; }

        [Display(Name = "کاربر")]
        public string UserName { get; set; }

        [Display(Name = "دوره")]
        public string CourseName { get; set; }

        [Display(Name = "گروه")]
        public string CourseGroupName { get; set; }

        [Display(Name = "گواهینامه")]
        public string CertificateName { get; set; }
    }
}
