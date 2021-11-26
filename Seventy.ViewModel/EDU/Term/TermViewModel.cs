using System;
using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU
{
    
    public class TermViewModel : CoreBaseViewModel 
    {
        [Display(Name = " دوره آموزشی")]
        public string CourseTitle { get; set; }

        [Display(Name = " گروه آموزشی")]
        public string GroupName { get; set; }

        [Display(Name = "عنوان ترم")]
        public string Title { get; set; }
		[Display(Name = "شروع")]
		public DateTime StartDate { get; set; }
		[Display(Name = "خاتمه")]
		public DateTime EndDate { get; set; }

        /// <summary>
        /// days
        /// </summary>
        [Display(Name = "مدت ترم")]
		public int Duration { get; set; }

        [Display(Name = "نام مدرس")]
        public string TeacherName { get; set; }

    }
}
