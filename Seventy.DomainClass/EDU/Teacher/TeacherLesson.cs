using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seventy.DomainClass.Core;
using System.ComponentModel;

namespace Seventy.DomainClass.EDU.Teacher
{
    public class TeacherLesson : Core.CoreBase
    {
        [DisplayName("استاد")]
        public int TeacherID { get; set; }

        [DisplayName("درس")]
        public int LessonID { get; set; }

        public Users RegUser { get; set; }
        public Users Teacher { get; set; }
        public Lesson.Lesson Lesson { get; set; }
    }
    public class TeacherLessonConfiguration : IEntityTypeConfiguration<TeacherLesson>
    {
        public void Configure(EntityTypeBuilder<TeacherLesson> builder)
        {
            builder.ToTable("TeacherLesson", "EDU");

            builder.HasIndex(e => e.LessonID)
                .HasName("UK_TeacherLesson")
                .IsUnique();

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.TeacherLessonRegUser)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TeacherLesson_Users");

            builder.HasOne(d => d.Teacher)
                .WithMany(p => p.TeacherLessonTeacher)
                .HasForeignKey(d => d.TeacherID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TeacherLesson_TeacherID_Users");
        }
    }
}
