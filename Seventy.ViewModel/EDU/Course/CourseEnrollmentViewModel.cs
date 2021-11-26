using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU
{
    
    public class CourseEnrollmentViewModel  
    {
		[Display(Name = "عنوان دوره")]
		public string Title { get; set; }
		[Display(Name ="نوع دوره")]
		public string CourseType { get; set; } //تک مهارتی / بلند مدت
		[Display(Name = "مدت دوره")]
		public int Duration { get; set; }		
		[Display(Name = "مبلغ دوره")]
		public int Price { get; set; }
		[Display(Name ="ثبت نام شده")]
		public bool IsRegistered { get; set; }
		[Display(Name = "تصویر دوره")]
		public string PhotoPath { get; set; }
		[Display(Name = "تصویر دوره")]
		public IFormFile Photo { get; set; }

    }
}
