using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU
{
    public class LessonViewModel : CoreBaseViewModel 
    {
        [Display(Name ="عنوان")]
        public string Title { get; set; }
        [Display(Name = "دوره")]
        public int CourseID { get; set; }
        [Display(Name = "ترم")]
        public int TermID { get; set; }
        [Display(Name = "تصویر درس")]
        public int? PicFileID { get; set; }

        public string PhotoPath { get; set; }
    }
}
