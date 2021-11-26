using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seventy.Common.Utilities;
using Seventy.DomainClass.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Seventy.DomainClass.EDU.Exam
{
    public class Exam : CoreBase
    {
        public int LessonID { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int QuestionCount { get; set; }
        public int PassingGrade { get; set; }
        public bool RandomQuestionsOrder { get; set; }
        public bool RandomQuestionOptionsOrder { get; set; }
        public int? FileID { get; set; }
        public int Time { get; set; }
        public Lesson.Lesson Lesson { get; set; }
        public Users RegUser { get; set; }
        public ICollection<ExamQuestions> ExamQuestions { get; set; }
        public ICollection<ExamUser> ExamUser { get; set; }
        [NotMapped]
        public bool Editable
        {
            get
            {
                return (StartDate > DateTime.Now);
            }
        }
        [NotMapped]
        public bool CanStart
        {
            get
            {
                var now = DateTime.Now;
                return (StartDate <= now && EndDate > now);
            }
        }
    }
    public class ExamConfiguration : IEntityTypeConfiguration<Exam>
    {
        public void Configure(EntityTypeBuilder<Exam> builder)
        {
            builder.ToTable("Exam", "EDU");

            builder.Property(e => e.EndDate).HasColumnType("datetime2(0)");

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.StartDate).HasColumnType("datetime2(0)");

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Type)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(d => d.Lesson)
                .WithMany(p => p.Exam)
                .HasForeignKey(d => d.LessonID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Exam_Lesson");

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.Exam)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Exam_Users");
        }
    }
}
