using System;
using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU
{
    public class LMSViewModel : CoreBaseViewModel 
    {
		[Display(Name = "عنوان")]
		public string Title { get; set; }
		[Display(Name = "آدرس URL")]
		public string URL { get; set; }
		[Display(Name = "شروع")]
		public DateTime StartDate { get; set; }
		[Display(Name = "خاتمه")]
		public DateTime EndDate { get; set; }
		[Display(Name = "وضعیت")]
		public string Status { get; set; }
        
    }
}
