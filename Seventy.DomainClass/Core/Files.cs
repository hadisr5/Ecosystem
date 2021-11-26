using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seventy.DomainClass.EDU;
using Seventy.DomainClass.EDU.Course;
using Seventy.DomainClass.EDU.Exam;
using Seventy.DomainClass.EDU.Exercise;
using Seventy.DomainClass.EDU.Lesson;
using Seventy.DomainClass.EDU.TrainingContent;
using System;
using System.Collections.Generic;

namespace Seventy.DomainClass.Core
{
    public class Files : CoreBase
    {
        public int UserID { get; set; }
        public string Title { get; set; }
        public string FileExtension { get; set; }
        public int Type { get; set; }

        public Users RegUser { get; set; }
        public Users User { get; set; }
        public ICollection<Certificate> Certificate { get; set; }
        public ICollection<Course> Course { get; set; }
        public ICollection<ExerciseUser> ExerciseUser { get; set; }
        public ICollection<Lesson> Lesson { get; set; }
        public ICollection<QuestionOptions> QuestionOptions { get; set; }
        public ICollection<Questions> Questions { get; set; }
        public ICollection<TrainingContent> TrainingContent { get; set; }
        public ICollection<UserDocuments> UserDocuments { get; set; }
        public ICollection<UserProfiles> UserProfiles { get; set; }
    }
    public class FilesConfiguration : IEntityTypeConfiguration<Files>
    {
        public void Configure(EntityTypeBuilder<Files> builder)
        {
            builder.ToTable("Files", "Core");

            builder.Property(e => e.FileExtension)
                  .IsRequired()
                  .HasMaxLength(10)
                  .IsUnicode(false);

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.FilesRegUser)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Files_RegUserID_Users");

            builder.HasOne(d => d.User)
                .WithMany(p => p.FilesUser)
                .HasForeignKey(d => d.UserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Files_Users");
        }
    }
}
