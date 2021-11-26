using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU
{

    public class TrainingWeekListViewModel : CoreBaseViewModel
    {

        [Display(Name = "درس گفتار")]
        public string Title { get; set; }


        [Display(Name = "درس")]
        public string LessonName { get; set; }

        [Display(Name = "ترم")]
        public string TermName { get; set; }
    }
}
