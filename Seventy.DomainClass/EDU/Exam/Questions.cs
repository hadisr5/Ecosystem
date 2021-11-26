using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seventy.DomainClass.Core;
using Seventy.DomainClass.EDU.Lesson;
using System;
using System.Collections.Generic;

namespace Seventy.DomainClass.EDU.Exam
{
    public class Questions : Core.CoreBase
    {
        public int LessonID { get; set; }
        public int QuestionLevel { get; set; }
        public string Title { get; set; }
        public bool MultiOption { get; set; }
        public int? FileID { get; set; }

        public Files File { get; set; }
        public Lesson.Lesson Lesson { get; set; }
        public Users RegUser { get; set; }
        public ICollection<ExamQuestions> ExamQuestions { get; set; }
        public ICollection<QuestionOptions> QuestionOptions { get; set; }
    }
    public class QuestionsConfiguration : IEntityTypeConfiguration<Questions>
    {
        public void Configure(EntityTypeBuilder<Questions> builder)
        {
            builder.ToTable("Questions", "EDU");

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Title).IsRequired();

            builder.HasOne(d => d.File)
                .WithMany(p => p.Questions)
                .HasForeignKey(d => d.FileID)
                .HasConstraintName("FK_Questions_Files");

            builder.HasOne(d => d.Lesson)
                .WithMany(p => p.Questions)
                .HasForeignKey(d => d.LessonID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Questions_Lesson");

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.Questions)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Questions_Users");
        }
    }
}
