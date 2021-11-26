using System;
using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU
{
    
    public class TermEditModel : CoreBaseViewModel 
    {
        [Display(Name = "عنوان ترم")]
        [Required]
        public string Title { get; set; }
        [Display(Name = " دوره آموزشی")]
        [Required]
        public int CourseID { get; set; }
        [Display(Name = " گروه آموزشی")]
        [Required]
        public int CourseGroupID { get; set; }
        [Display(Name = "شروع")]
        [Required]
        public DateTime StartDate { get; set; }
        [Display(Name = "خاتمه")]
        [Required]
        public DateTime EndDate { get; set; }
        /// <summary>
        /// days
        /// </summary>
        [Display(Name = "مدت ترم")]
        [Required]
        public int Duration { get; set; }


    }
}
