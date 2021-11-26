using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Seventy.DomainClass.EDU.TrainingWeek
{
    public class TrainingWeek : Core.CoreBase
    {
        public int LessonID { get; set; }
        public int TermID { get; set; }
        public string Title { get; set; }

        public Lesson.Lesson Lesson { get; set; }
        public Term.Term Term { get; set; }
        public ICollection<Poll.Poll> Poll { get; set; }
        public ICollection<TrainingWeekContent> TrainingWeekContent { get; set; }
        public ICollection<UserTrainingWeekContent> UserTrainingWeekContent { get; set; }
    }
    public class TrainingWeekConfiguration : IEntityTypeConfiguration<TrainingWeek>
    {
        public void Configure(EntityTypeBuilder<TrainingWeek> builder)
        {
            builder.ToTable("TrainingWeek", "EDU");

            builder.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(d => d.Lesson)
                .WithMany(p => p.TrainingWeek)
                .HasForeignKey(d => d.LessonID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TrainingWeek_Lesson");

            builder.HasOne(d => d.Term)
                .WithMany(p => p.TrainingWeeks)
                .HasForeignKey(d => d.TermID)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
