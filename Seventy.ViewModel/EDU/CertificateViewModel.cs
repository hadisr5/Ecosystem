
namespace Seventy.ViewModel.EDU
{
    using System.ComponentModel.DataAnnotations;

    public class CertificateViewModel : CoreBaseViewModel
    {
        [Display(Name ="عنوان")]
        public string Title { get; set; }
		[Display(Name = "نوع")]
		public string Type { get; set; }
		[Display(Name = "فایل نمونه")]
		public int SampleFileID { get; set; }
		[Display(Name = "سازمان اعتباربخش")]
		public string CreditorOrganization { get; set; }
    }
}
