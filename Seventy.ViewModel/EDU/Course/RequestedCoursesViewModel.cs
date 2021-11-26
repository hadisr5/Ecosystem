using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU
{
    public class RequestedCoursesViewModel : CoreBaseViewModel 
    {
        [Display(Name = "کاربر")]
        public int UserID { get; set; }
		[Display(Name = "دسته بندی")]
		public string Category { get; set; }
		[Display(Name = "عنوان دوره")]
		public string Title { get; set; }
   
    }
}
