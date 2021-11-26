using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.Core
{
    public class FilesViewModel : CoreBaseViewModel
    {
        [Display(Name = "عنوان")]
        public string Title { get; set; }
        [Display(Name = "کاربر")]
        public int UserID { get; set; }

        [Display(Name = "نوع فایل")]
        public int Type { get; set; }

        [Display(Name = "انتخاب فایل")]
        public IFormFile UploadFile { get; set; }
    }

    public class FilesSecondViewModel : CoreBaseViewModel
    {
        [Display(Name = "کاربر")]
        public int UserID { get; set; }

        [Display(Name = "عنوان")]
        public string Title { get; set; }

        [Display(Name = "اکستنشن")]
        public string FileExtension { get; set; }

        [Display(Name = "فایل")]
        public int? FileId { get; set; }
        public string FileContent { get; set; }
    }
}
