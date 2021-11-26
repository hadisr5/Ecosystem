using System;
using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU
{
    public class ExamUserViewModel : CoreBaseViewModel
    {
        [Display(Name = "آزمون")]
        public int ExamID { get; set; }
        [Display(Name = "آزمون")]
        public string ExamTitle { get; set; }
        [Display(Name = "کاربر")]
        public int UserID { get; set; }
        [Display(Name = "نام فرد")]
        public string UserNameAndFamily { get; set; }
        [Display(Name = "فایل")]
        public int? FileID { get; set; }
        [Display(Name = "نتیجه")]
        public double? Result { get; set; }
        [Display(Name = "میزان رضایت")]
        public int? LikeRank { get; set; }

        [Display(Name = "درس")]
        public string LessonTitle { get; set; }

        [Display(Name = "زمان شروع")]
        public DateTime StartDate { get; set; }

        [Display(Name = "زمان پایان")]
        public DateTime EndDate { get; set; }

        [Display(Name = "مدت زمان (دقیقه)")]
        public int Time { get; set; }
        public Seventy.DomainClass.EDU.Exam.Exam Exam { get; set; }

    }
}
