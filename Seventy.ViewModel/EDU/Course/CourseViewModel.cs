using Microsoft.VisualBasic;
using Seventy.ViewModel.EDU.Main;
using System;
using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU.Course
{
    
    public class CourseViewModel : CoreBaseViewModel 
    {
		[Display(Name = "عنوان دوره")]
		public string Title { get; set; }

		[Display(Name ="نوع دوره")]
		public string CourseType { get; set; }

		[Display(Name = "مدارک مورد نیاز")]
		public string RequiredDocuments { get; set; }

		[Display(Name = "دسته بندی")]
		public int CategoryID { get; set; }

		[Display(Name = "دسته بندی")]
		public string CategoryTitle { get; set; }

		[Display(Name = "مدت دوره")]
		public int Duration { get; set; }

		[Display(Name = "وضعیت انتشار")]
		public string PublishState { get; set; }

		[Display(Name = "دستاوردهای دوره")]
		public string Achievements { get; set; }

		[Display(Name = "نوع حضور")]
		public string HozoriType { get; set; }

		[Display(Name = "مبلغ دوره")]
		public int Price { get; set; }

		[Display(Name = "تصویر دوره")]
		public string PhotoURL { get; set; }

		[Display(Name = "مدرس دوره")]
		public string CourseTeacher { get; set; }

        [Display(Name = "تصویر دوره")]
        public int? PhotoFileID { get; set; }
    }
}
