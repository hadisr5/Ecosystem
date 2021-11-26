using Seventy.ViewModel;
using System.ComponentModel.DataAnnotations;

namespace Seventy.DomainClass.EDU
{
    
    public class TrainingEvalResultEditModel : CoreBaseViewModel
    {
        [Display(Name = "هدف ارزیابی")]
        public int TrainingEvalIndexID { get; set; }

        [Display(Name = "کاربر")]
        public int UserID { get; set; }

        [Display(Name = "نتیجه")]
        public int Result { get; set; }

        
    }
}
