using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seventy.DomainClass.Core;

namespace Seventy.DomainClass.EDU.Lesson
{
    public class LessonObservation : Core.CoreBase
    {
        public int UserID { get; set; }
        public int LessonID { get; set; }

        public Lesson Lesson { get; set; }
        public Users RegUser { get; set; }
        public Users User { get; set; }
    }
    public class LessonObservationConfiguration : IEntityTypeConfiguration<LessonObservation>
    {
        public void Configure(EntityTypeBuilder<LessonObservation> builder)
        {
            builder.ToTable("LessonObservation", "EDU");

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.HasOne(d => d.Lesson)
                .WithMany(p => p.LessonObservation)
                .HasForeignKey(d => d.LessonID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LessonObservation_Lesson");

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.LessonObservationRegUser)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LessonObservation_RegUserID_Users");

            builder.HasOne(d => d.User)
                .WithMany(p => p.LessonObservationUser)
                .HasForeignKey(d => d.UserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LessonObservation_Users");
        }
    }
}
