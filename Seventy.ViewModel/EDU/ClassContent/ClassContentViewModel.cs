
using Seventy.DomainClass.EDU.TrainingWeek;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU
{
    public class ClassContentViewModel : CoreBaseViewModel
    {
        public int UserID { get; set; }
        public int CourseID { get; set; }
        public int CourseGroupID { get; set; }
        public int TermID { get; set; }
        public int LessonID { get; set; }
        public int WeekID { get; set; }

        public List<UserTrainingWeekContent> WatchedContent { get; set; }
    }
}
