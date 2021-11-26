using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Seventy.DomainClass.EDU.Exercise
{
    public class Exercise : Core.CoreBase
    {
        public int LessonID { get; set; }
        public int FileID { get; set; }
        public int Barom { get; set; }
        public string CorrectAnswer { get; set; }
        public int ContentID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Lesson.Lesson Lesson { get; set; }
        public ICollection<ExerciseUser> ExerciseUser { get; set; }
    }
    public class ExerciseConfiguration : IEntityTypeConfiguration<Exercise>
    {
        public void Configure(EntityTypeBuilder<Exercise> builder)
        {
            builder.ToTable("Exercise", "EDU");

            builder.Property(e => e.CorrectAnswer).IsRequired();

            builder.Property(e => e.Description).IsRequired();

            builder.Property(e => e.EndDate).HasColumnType("datetime2(0)");

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.StartDate).HasColumnType("datetime2(0)");

            builder.HasOne(d => d.Lesson)
                .WithMany(p => p.Exercise)
                .HasForeignKey(d => d.LessonID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Exercise_Lesson");
        }
    }
}
