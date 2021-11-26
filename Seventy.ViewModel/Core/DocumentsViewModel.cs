using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.Core
{
    public class DocumentsViewModel : CoreBaseViewModel
    {
        [Display(Name = "کاربر")]
        public int UserId { get; set; }

        [Display(Name = "بخش")]
        public string Section { get; set; }

        [Display(Name = "نوع سند")]
        public string DocType { get; set; }

        [Display(Name = "فایل")]
        public string FilePath { get; set; }
    }
}
