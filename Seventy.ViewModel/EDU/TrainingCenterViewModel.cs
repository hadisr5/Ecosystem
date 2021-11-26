using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU
{
    
    public class TrainingCenterViewModel : CoreBaseViewModel 
    {
        [Display(Name = "عنوان مرکز آموزشی")]
        public string Title { get; set; }
		[Display(Name = "مکان")]
		public int PlaceID { get; set; }

    }
}
