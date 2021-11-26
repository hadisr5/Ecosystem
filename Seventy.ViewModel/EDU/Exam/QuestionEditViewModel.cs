using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU.Exam
{
    public class QuestionEditViewModel : CoreBaseViewModel
    {
        [Display(Name = "درس")]
        public int LessonID { get; set; }

        [Display(Name = "سطح سوال")]
        public int QuestionLevel { get; set; }

        [Display(Name = "متن سوال")]
        [Required(ErrorMessage = "متن سوال را وارد نمائید")]
        public string Title { get; set; }

        [Display(Name = "چند گزینه‌ای")]
        public bool MultiOption { get; set; } = false;

        [Display(Name = "فایل")]
        public int? FileID { get; set; }
    }
}
