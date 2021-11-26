using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU
{
    
    public class TrainingWeekEditModel : CoreBaseViewModel 
    {
        [Display(Name = "درس")]
        [Required]
        public int LessonID { get; set; }

        [Display(Name = "ترم")]
        [Required]
        public int TermID { get; set; }


        [Required]
        [Display(Name = "درس گفتار")]
        public string Title { get; set; }


    }
}
