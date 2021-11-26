using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU
{
    public class ContentObservationViewModel : CoreBaseViewModel 
    {
        [Display(Name = "کاربر")]
        public int UserID { get; set; }
		[Display(Name = "محتوی")]
		public int ContentID { get; set; }
        
    }
}
