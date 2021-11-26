using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU.TrainingWeek
{
    
    public class ContentFileViewModel : CoreBaseViewModel 
    {
        [Display(Name = "فایل")]
		public int? FileId { get; set; }

    }
}
