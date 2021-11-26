using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU
{
    
    public class TrainingWeekContentViewModel : CoreBaseViewModel 
    {
        public int TrainingWeekID { get; set; }
        public int ContentID { get; set; }
        [Display(Name = "درس گفتار")]

        public string TrainingWeekTitle { get; set; }
        [Display(Name = "نوع محتوا")]
        public string ContentType { get; set; }

        [Display(Name = "محتوای آموزشی")]
		public string ContentTitle { get; set; }

    }
}
