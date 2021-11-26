using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU.Lesson
{
        public class LessonEditViewModel : CoreBaseViewModel
        {
            [Display(Name = "عنوان")]
            public string Title { get; set; }

            [Display(Name = "تصویر درس")]
            public int? PicFileID { get; set; }
    }
}
