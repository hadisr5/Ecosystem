using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU
{
    
    public class CourseCategoryEditModel : CoreBaseViewModel 
    {
        [Display(Name = "دسته اصلی")]
        public string PrimaryCat { get; set; }
		[Display(Name = "دسته فرعی")]
		public string SecondaryCat { get; set; }

    }
}
