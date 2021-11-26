using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seventy.DomainClass.Core;

namespace Seventy.DomainClass.EDU.Course
{
    public class RelatedCourses : CoreBase
    {
        public int FirstCourseID { get; set; }
        public int SecondCourseID { get; set; }

        public Course FirstCourse { get; set; }
        public Users RegUser { get; set; }
        public Course SecondCourse { get; set; }
    }
    public class RelatedCoursesConfiguration : IEntityTypeConfiguration<RelatedCourses>
    {
        public void Configure(EntityTypeBuilder<RelatedCourses> builder)
        {
            builder.ToTable("RelatedCourses", "EDU");

            builder.HasIndex(e => new { e.FirstCourseID, e.SecondCourseID })
                    .HasName("UK_RelatedCourses")
                    .IsUnique();

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.HasOne(d => d.FirstCourse)
                .WithMany(p => p.RelatedCoursesFirstCourse)
                .HasForeignKey(d => d.FirstCourseID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RelatedCourses_FirstCourseID_Course");

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.RelatedCourses)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RelatedCourses_Users");

            builder.HasOne(d => d.SecondCourse)
                .WithMany(p => p.RelatedCoursesSecondCourse)
                .HasForeignKey(d => d.SecondCourseID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RelatedCourses_SecondCourseID_Course");
        }
    }
}
