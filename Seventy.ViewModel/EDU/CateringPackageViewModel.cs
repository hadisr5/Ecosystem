
using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU
{
    public class CateringPackageViewModel : CoreBaseViewModel
    {
        [Display(Name = "عنوان")]
        public string Title { get; set; }
		
        [Display(Name = "دسته بندی")]
		public string Category { get; set; }

        [Display(Name = "مبلغ")]
        public int Price { get; set; }
    }
}
