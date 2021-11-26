using System;
using System.Collections.Generic;

namespace Seventy.ViewModel.EDU.TrainingWeek.TrainingWeekSituationSummary
{
    public class LessonSituationSummary
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;
        public int? RegUserID { get; set; }
        public DateTime RegDate { get; set; }
        public string Title { get; set; }
        public int? PicFileID { get; set; }
        public List<WeekSituationSummary> TrainingWeeks { get; set; }
    }
}