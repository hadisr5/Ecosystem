using System;

namespace Seventy.ViewModel.EDU.TrainingWeek.TrainingWeekSituationSummary
{
    public class TrainingContentSituationSummary
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;
        public int? RegUserID { get; set; }
        public DateTime RegDate { get; set; }

        public string Title { get; set; }
        public string ContentType { get; set; }
        public int? ExternalContentID { get; set; }
        public int? FileID { get; set; }
        public string DemoState { get; set; }
        public string Achievement { get; set; }
    }
}