using Seventy.ViewModel;
using System.ComponentModel.DataAnnotations;

namespace Seventy.DomainClass.EDU
{
    
    public class TrainingEvalResultViewModel : CoreBaseViewModel
    {
        [Display(Name = "هدف ارزیابی")]
        public int TrainingEvalIndexID { get; set; }
        [Display(Name = "کاربر")]
        public int UserID { get; set; }
        [Display(Name = "نتیجه")]
        public int Result { get; set; }

        [Display(Name = "کاربر")]
        public string UserName { get; set; }

        [Display(Name = "هدف ارزیابی")]
        public string TrainingEvalIndexTitle { get; set; }
    }
}
