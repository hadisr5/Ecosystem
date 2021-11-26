using System;
using System.Collections.Generic;

namespace Seventy.ViewModel.EDU.TrainingWeek.TrainingWeekSituationSummary
{
    public class WeekSituationSummary
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;
        public int? RegUserID { get; set; }
        public DateTime RegDate { get; set; }
        public int LessonID { get; set; }
        public string Title { get; set; }
        public List<TrainingContentSituationSummary> TrainingContents { get; set; }
    }
}