using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Seventy.DomainClass.EDU.Exam;

namespace Seventy.ViewModel.EDU.Exam.ExamAnswerSheet
{
    public class ExamAnswerSheetViewModel : CoreBaseViewModel
    {
        [Display(Name = "ردیف")]
        public int RowNumber { get; set; }
        [Display(Name = "آزمون")]
        public int ExamID { get; set; }
        [Display(Name = "کاربر")]
        public int UserID { get; set; }
        [Display(Name = "سوال")]
        public int QuestionID { get; set; }
        [Display(Name = "بارم سوال")]
        public float QuestionBarom { get; set; }
        [Display(Name = "پاسخ")]
        public string Answer { get; set; }
        [Display(Name = "بارم بدست آمده")]
        public double? AchievedBarom { get; set; }
        [Display(Name = "آزمون")]
        public string ExamTitle { get; set; }

        [Display(Name = "سوال")]
        public string QuestionTitle { get; set; }

        [Display(Name = "نوع")]
        public bool QuestionType { get; set; }
    }
}
