using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Seventy.DomainClass.EDU.Exam
{
    public  class ExamAnswerSheet : Core.CoreBase
    {
        public int ExamID { get; set; }
        public int UserID { get; set; }
        public int QuestionID { get; set; }
        public string Answer { get; set; }
        public int? AnswerOption { get; set; }
        public double? AchievedBarom { get; set; }
        public int? FileID { get; set; }
    }
    public class ExamAnswerSheetConfiguration : IEntityTypeConfiguration<ExamAnswerSheet>
    {
        public void Configure(EntityTypeBuilder<ExamAnswerSheet> builder)
        {
            builder.ToTable("ExamAnswerSheet", "EDU");

            builder.Property(e => e.Answer).IsRequired();

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");
        }
    }
}
