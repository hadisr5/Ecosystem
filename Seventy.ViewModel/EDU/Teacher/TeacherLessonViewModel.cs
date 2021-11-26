using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU
{
    public class TeacherLessonViewModel : CoreBaseViewModel 
    {

        public int TeacherID { get; set; }

		public int LessonID { get; set; }

        [Display(Name = "نام مدرس")]
        public string TeacherName { get; set; }

        [Display(Name = "نام درس")]
        public string LessonName { get; set; }

        [Display(Name = "عکس درس")]
        public int? PicId { get; set; }
    }
}
