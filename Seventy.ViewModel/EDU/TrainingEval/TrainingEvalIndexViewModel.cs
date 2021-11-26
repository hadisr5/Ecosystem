using Seventy.ViewModel;
using System.ComponentModel.DataAnnotations;

namespace Seventy.DomainClass.EDU
{
    
    public class TrainingEvalIndexViewModel : CoreBaseViewModel
    {
        [Display(Name = "عنوان شاخص")]
        [Required]
        public string Title { get; set; }

        [Display(Name = "شاخص هدف")]
        public string TargetType { get; set; }

        [Display(Name = "هدف ارزیابی")]
        public int TargetID { get; set; }

        [Display(Name = "هدف ارزیابی")]
        public string TargetName { get; set; }
    }
}
