using Seventy.ViewModel;
using System.ComponentModel.DataAnnotations;

namespace Seventy.DomainClass.Core
{
    public class RolesViewModel : CoreBaseViewModel
    {
        [Display(Name = "عنوان")]
        public string Title { get; set; }

        [Display(Name = "الویت")]
        public int Priority { get; set; }
    }
}
