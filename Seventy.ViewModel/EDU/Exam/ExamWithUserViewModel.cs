using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU.Exam
{
    public class ExamWithUserViewModel : CoreBaseViewModel
    {
        [Display(Name = "آزمون")]
        public int ExamID { get; set; }

        [Display(Name = "آزمون")]
        public string ExamTitle { get; set; }

        [Display(Name = "کاربر")]
        public int UserID { get; set; }

        [Display(Name = "نام فرد")]
        public string UserNameAndFamily { get; set; }
    }
}
