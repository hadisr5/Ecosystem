using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seventy.DomainClass.Core;

namespace Seventy.DomainClass.EDU.Course
{
    public class CourseObservation : CoreBase
    {
        public int UserID { get; set; }
        public int CourseID { get; set; }

        public Course Course { get; set; }
        public Users RegUser { get; set; }
        public Users User { get; set; }
    }
    public class CourseObservationConfiguration : IEntityTypeConfiguration<CourseObservation>
    {
        public void Configure(EntityTypeBuilder<CourseObservation> builder)
        {
            builder.ToTable("CourseObservation", "EDU");

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.HasOne(d => d.Course)
                .WithMany(p => p.CourseObservation)
                .HasForeignKey(d => d.CourseID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CourseObservation_Course");

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.CourseObservationRegUser)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CourseObservation_RegUserID_Users");

            builder.HasOne(d => d.User)
                .WithMany(p => p.CourseObservationUser)
                .HasForeignKey(d => d.UserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CourseObservation_Users");
        }
    }
}
