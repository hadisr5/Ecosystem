using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seventy.DomainClass.Core;
using Seventy.DomainClass.EDU.Exam;
using Seventy.DomainClass.EDU.Teacher;
using Seventy.DomainClass.EDU.Term;
using Seventy.DomainClass.EDU.TrainingContent;
using Seventy.DomainClass.EDU.TrainingWeek;
using System.Collections.Generic;

namespace Seventy.DomainClass.EDU.Lesson
{
    public class Lesson : Core.CoreBase
    {
        public string Title { get; set; }
        public int? PicFileID { get; set; }

        public Files PicFile { get; set; }
        public Users RegUser { get; set; }
        public ICollection<Exam.Exam> Exam { get; set; }
        public ICollection<Exercise.Exercise> Exercise { get; set; }
        public ICollection<Forum> Forum { get; set; }
        public ICollection<LessonObservation> LessonObservation { get; set; }
        public ICollection<Questions> Questions { get; set; }
        public ICollection<RequestForContent> RequestForContent { get; set; }
        public ICollection<TeacherLike> TeacherLike { get; set; }
        public ICollection<TrainingWeek.TrainingWeek> TrainingWeek { get; set; }
        public ICollection<UserTrainingWeekContent> UserTrainingWeekContent { get; set; }
        public ICollection<TermLesson> TermLessons { get; set; }
    }
    public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.ToTable("Lesson", "EDU");

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(d => d.PicFile)
                .WithMany(p => p.Lesson)
                .HasForeignKey(d => d.PicFileID)
                .HasConstraintName("FK_Lesson_Files");

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.Lesson)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Lesson_Users");
        }
    }
}
