using System;

namespace Seventy.DomainClass.EDU
{
    using Seventy.ViewModel;
    using System.ComponentModel.DataAnnotations;

    public class CourseGroupsViewModel : CoreBaseViewModel
    {
        public int CourseID { get; set; }

        [Display(Name = "عنوان گروه")]
        public string Title { get; set; }

        [Display(Name = " تاریخ شروع")]
        public DateTime StartDate { get; set; }

        [Display(Name = " تاریخ اتمام")]
        public DateTime EndDate { get; set; }

        [Display(Name = " ظرفیت")]
        public int Capacity { get; set; }


        /// <summary>
        /// این فیلد فقط در نمایش لیست گروه ها استفاده میشود
        /// در هنگام ثبت نیازی نیست پر شود 
        /// </summary>
        [Display(Name = " عنوان دوره")]
        public string CourseName { get; set; }

    }
}
