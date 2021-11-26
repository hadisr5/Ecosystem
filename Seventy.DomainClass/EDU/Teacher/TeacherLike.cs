using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seventy.DomainClass.Core;

namespace Seventy.DomainClass.EDU.Teacher
{
    public class TeacherLike : Core.CoreBase
    {
        public int UserID { get; set; }
        public int TeacherID { get; set; }
        public int LessonID { get; set; }
        public int LikeRank { get; set; }

        public Lesson.Lesson Lesson { get; set; }
        public Users RegUser { get; set; }
        public Users Teacher { get; set; }
        public Users User { get; set; }
    }
    public class TeacherLikeConfiguration : IEntityTypeConfiguration<TeacherLike>
    {
        public void Configure(EntityTypeBuilder<TeacherLike> builder)
        {
            builder.ToTable("TeacherLike", "EDU");

            builder.Property(e => e.RegDate).HasColumnType("datetime2(0)");

            builder.HasOne(d => d.Lesson)
                .WithMany(p => p.TeacherLike)
                .HasForeignKey(d => d.LessonID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TeacherLike_Lesson");

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.TeacherLikeRegUser)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TeacherLike_RegUserID_Users");

            builder.HasOne(d => d.Teacher)
                .WithMany(p => p.TeacherLikeTeacher)
                .HasForeignKey(d => d.TeacherID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TeacherLike_TeacherID_Users");

            builder.HasOne(d => d.User)
                .WithMany(p => p.TeacherLikeUser)
                .HasForeignKey(d => d.UserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TeacherLike_Users");
        }
    }
}
