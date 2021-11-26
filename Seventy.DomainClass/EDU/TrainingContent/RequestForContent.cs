using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seventy.DomainClass.Core;

namespace Seventy.DomainClass.EDU.TrainingContent
{
    public class RequestForContent : Core.CoreBase
    {
        public int UserID { get; set; }
        public int LessonID { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }

        public Lesson.Lesson Lesson { get; set; }
        public Users RegUser { get; set; }
        public Users User { get; set; }
    }
    public class RequestForContentConfiguration : IEntityTypeConfiguration<RequestForContent>
    {
        public void Configure(EntityTypeBuilder<RequestForContent> builder)
        {
            builder.ToTable("RequestForContent", "EDU");

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(d => d.Lesson)
                .WithMany(p => p.RequestForContent)
                .HasForeignKey(d => d.LessonID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RequestForContent_Lesson");

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.RequestForContentRegUser)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RequestForContent_RegUserID_Users");

            builder.HasOne(d => d.User)
                .WithMany(p => p.RequestForContentUser)
                .HasForeignKey(d => d.UserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RequestForContent_Users");
        }
    }
}
