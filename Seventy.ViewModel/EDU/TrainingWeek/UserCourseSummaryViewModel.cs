using Seventy.DomainClass.EDU.Term;
using Seventy.DomainClass.EDU.TrainingContent;
using Seventy.DomainClass.EDU.TrainingWeek;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU.TrainingWeek
{
    /// <summary>
    /// 
    /// </summary>
    public class UserCourseSummaryViewModel : CoreBaseViewModel 
    {
        public string CourseTitle { get; set; }
        public string CourseGroupTitle { get; set; }
        public Term Term { get; set; }
        //این ویومدل برای استفاده در نمودار پیشرفت فراگیر در کلاس ایجاد شده است
        public List<DomainClass.EDU.Lesson.Lesson> Lessons { get; set; }
        public List<DomainClass.EDU.TrainingWeek.TrainingWeek> TrainingWeeks { get; set; }
        public List<TrainingWeekContentViewModel> TrainingWeekContents { get; set; }
        public List<UserTrainingWeekContentViewModel> UserTrainingWeekContents { get; set; }

    }
}
