using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seventy.DomainClass.Core;
using System;
using System.Collections.Generic;

namespace Seventy.DomainClass.EDU.Exam
{
    public class ExamQuestions : Core.CoreBase
    {
        public int ExamID { get; set; }
        public int QuestionID { get; set; }
        public int Barom { get; set; }

        public Exam Exam { get; set; }
        public Questions Question { get; set; }
        public Users RegUser { get; set; }
    }
    public class ExamQuestionsConfiguration : IEntityTypeConfiguration<ExamQuestions>
    {
        public void Configure(EntityTypeBuilder<ExamQuestions> builder)
        {
            builder.ToTable("ExamQuestions", "EDU");

            builder.Property(e => e.RegDate)
                    .HasColumnType("datetime2(0)")
                    .HasDefaultValueSql("(getdate())");

            builder.HasOne(d => d.Exam)
                .WithMany(p => p.ExamQuestions)
                .HasForeignKey(d => d.ExamID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ExamQuestions_Exam");

            builder.HasOne(d => d.Question)
                .WithMany(p => p.ExamQuestions)
                .HasForeignKey(d => d.QuestionID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ExamQuestions_Questions");

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.ExamQuestions)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ExamQuestions_Users");

        }
    }
}
