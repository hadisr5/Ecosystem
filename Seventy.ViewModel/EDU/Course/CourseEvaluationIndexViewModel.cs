using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU
{
    public class CourseEvaluationIndexViewModel : CoreBaseViewModel
    {
        [Display(Name = "دوره")]
        public int CourseID { get; set; }
		[Display(Name = "دسته بندی شاخص")]
		public string IndexCategory { get; set; }

    }
}
