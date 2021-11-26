namespace Seventy.ViewModel.EDU
{
    using Seventy.ViewModel;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class QuestionOptionsViewModel : CoreBaseViewModel
    {
        public int QuestionID { get; set; }

        [Display(Name = "عنوان")]
        public string Title { get; set; }

        [Display(Name = "انتخاب فایل")]
        [Required(ErrorMessage ="لطفا فایل خود را انتخاب کنید")]
        public int? FileID { get; set; }

        [Display(Name = "گزینه صحیح")]
        public bool IsCorrect { get; set; }
    }
}
