using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seventy.DomainClass.Core;

namespace Seventy.DomainClass.EDU.Course
{
    public class RequestedCourses : CoreBase
    {
        public int UserID { get; set; }

        [DisplayName("نوع")]
        public string CourseType { get; set; }

        [DisplayName("عنوان")]
        public string Title { get; set; }

        [DisplayName("وضعیت")]
        public string Status { get; set; }

        public Users RegUser { get; set; }
    }
    public class RequestedCoursesConfiguration : IEntityTypeConfiguration<RequestedCourses>
    {
        public void Configure(EntityTypeBuilder<RequestedCourses> builder)
        {
            builder.ToTable("RequestedCourses", "EDU");

            builder.Property(e => e.CourseType)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Status).HasMaxLength(50);

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.RequestedCourses)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RequestedCourses_RegUserID_Users");
        }
    }
}
