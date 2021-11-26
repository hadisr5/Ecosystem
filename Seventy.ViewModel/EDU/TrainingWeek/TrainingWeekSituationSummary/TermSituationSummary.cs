using System;
using System.Collections.Generic;

namespace Seventy.ViewModel.EDU.TrainingWeek.TrainingWeekSituationSummary
{
    public class TermSituationSummary
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;
        public int? RegUserID { get; set; }
        public DateTime RegDate { get; set; }
        public string Title { get; set; }
        public int CourseID { get; set; }
        public int CourseGroupID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Duration { get; set; }
        public List<LessonSituationSummary> Lessons { get; set; }

    }
}