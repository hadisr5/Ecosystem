using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using Seventy.DomainClass.Core;

namespace Seventy.DomainClass.EDU.Term
{
    public class TermLesson : Core.CoreBase
    {
        public int CourseID { get; set; }
        public int CourseGroupID { get; set; }
        public int TermID { get; set; }
        public int LessonID { get; set; }
        public int TeacherID { get; set; }

        public Course.Course Course { get; set; }
        public Course.CourseGroups CourseGroup { get; set; }
        public Term Term { get; set; }
        public Lesson.Lesson Lesson { get; set; }
        public Users Teacher { get; set; }
    }
    public class TermLessonConfiguration : IEntityTypeConfiguration<TermLesson>
    {
        public void Configure(EntityTypeBuilder<TermLesson> builder)
        {
            builder.ToTable("TermLesson", "EDU");

            builder.Property(e => e.RegDate).HasColumnType("datetime2(0)");

            builder.HasOne(o => o.Course)
                .WithMany(m => m.TermLessons)
                .HasForeignKey(f => f.CourseID)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(o => o.CourseGroup)
                .WithMany(m => m.TermLessons)
                .HasForeignKey(f => f.CourseGroupID)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(o => o.Term)
                .WithMany(m => m.TermLessons)
                .HasForeignKey(f => f.TermID)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(o => o.Lesson)
                .WithMany(m => m.TermLessons)
                .HasForeignKey(f => f.LessonID)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(o => o.Teacher)
                .WithMany(m => m.TermLessons)
                .HasForeignKey(f => f.TeacherID)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
