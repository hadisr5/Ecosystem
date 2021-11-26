using Seventy.DomainClass.Core;
using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.Core
{
    public class DocumentTypeEditModel : CoreBaseViewModel
    {
        [Display(Name ="عنوان")]
        [Required]
        public string Title { get; set; }
    }
}