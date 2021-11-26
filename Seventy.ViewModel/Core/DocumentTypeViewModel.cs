using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.Core
{
    
    public class DocumentTypeViewModel : CoreBaseViewModel
    {
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "عنوان مدرک را وارد کنید")]

        public string Title { get; set; }
    }
}
