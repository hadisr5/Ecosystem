using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU
{
    
    public class TrainingWeekContentEditModel : CoreBaseViewModel 
    {
        [Display(Name = "درس گفتار")]
        [Required]
        public int TrainingWeekID { get; set; }
        [Display(Name = "نوع محتوا")]
        [Required]
        public string ContentType { get; set; }
        [Display(Name = "محتوای آموزشی")]
        [Required]
        public int ContentID { get; set; }


    }
}
