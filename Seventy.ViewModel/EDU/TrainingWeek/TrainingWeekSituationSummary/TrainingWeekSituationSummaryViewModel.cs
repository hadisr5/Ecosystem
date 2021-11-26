using System;
using System.Collections.Generic;

namespace Seventy.ViewModel.EDU.TrainingWeek.TrainingWeekSituationSummary
{
    public class TrainingWeekSituationSummaryViewModel
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;
        public int? RegUserID { get; set; }
        public DateTime RegDate { get; set; }
        public string CourseTitle { get; set; }
        public string GroupTitle { get; set; }
        public int Progress { get; set; }
        public List<TermSituationSummary> Terms { get; set; }
        public List<string> Achievements { get; set; }

    }
}
