using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU
{
    public class ForumViewModel : CoreBaseViewModel 
    {
        [Display(Name = "دوره")]
        public int CourseID { get; set; }
		[Display(Name = "عنوان فروم")]
		public string Title { get; set; }

    }
}
