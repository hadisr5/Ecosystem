using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU.TrainingEval
{
    
    public class TrainingEvalIndexEditModel : CoreBaseViewModel
    {

        [Display(Name = "عنوان شاخص")]
        [Required]
        public string Title { get; set; }

        [Display(Name = "شاخص هدف")]
        public string TargetType { get; set; }

        [Display(Name = "هدف ارزیابی")]
        public int TargetID { get; set; }
    }
}
