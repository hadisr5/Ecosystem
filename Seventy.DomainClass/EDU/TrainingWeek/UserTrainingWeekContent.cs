using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seventy.DomainClass.Core;
using Seventy.DomainClass.EDU.Course;
using System;
using System.Collections.Generic;

namespace Seventy.DomainClass.EDU.TrainingWeek
{
    public class UserTrainingWeekContent : Core.CoreBase
    {
        public int CourseID { get; set; }
        public int CourseGroupID { get; set; }
        public int LessonID { get; set; }
        public int UserID { get; set; }
        public int TrainingWeekID { get; set; }
        public int ContentID { get; set; }
        public int? Progress { get; set; }
        public bool Result { get; set; }
        public int? LikeRank { get; set; }
        public int TermID { get; set; }

        public Term.Term Term { get; set; }
        public TrainingContent.TrainingContent Content { get; set; }
        public Course.Course Course { get; set; }
        public CourseGroups CourseGroup { get; set; }
        public Lesson.Lesson Lesson { get; set; }
        public Users RegUser { get; set; }
        public TrainingWeek TrainingWeek { get; set; }
        public Users User { get; set; }
    }
    public class UserTrainingWeekContentConfiguration : IEntityTypeConfiguration<UserTrainingWeekContent>
    {
        public void Configure(EntityTypeBuilder<UserTrainingWeekContent> builder)
        {
            builder.ToTable("UserTrainingWeekContent", "EDU");

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.HasOne(d => d.Term)
               .WithMany(p => p.UserTrainingWeekContents)
               .HasForeignKey(d => d.TermID)
               .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.Content)
                .WithMany(p => p.UserTrainingWeekContent)
                .HasForeignKey(d => d.ContentID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserTrainingWeekContent_TrainingContent");

            builder.HasOne(d => d.CourseGroup)
                .WithMany(p => p.UserTrainingWeekContent)
                .HasForeignKey(d => d.CourseGroupID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserTrainingWeekContent_CourseGroups");

            builder.HasOne(d => d.Course)
                .WithMany(p => p.UserTrainingWeekContent)
                .HasForeignKey(d => d.CourseID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserTrainingWeekContent_Course");

            builder.HasOne(d => d.Lesson)
                .WithMany(p => p.UserTrainingWeekContent)
                .HasForeignKey(d => d.LessonID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserTrainingWeekContent_Lesson");

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.UserTrainingWeekContentRegUser)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserTrainingWeekContent_RegUserID_Users");

            builder.HasOne(d => d.TrainingWeek)
                .WithMany(p => p.UserTrainingWeekContent)
                .HasForeignKey(d => d.TrainingWeekID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserTrainingWeekContent_TrainingWeek");

            builder.HasOne(d => d.User)
                .WithMany(p => p.UserTrainingWeekContentUser)
                .HasForeignKey(d => d.UserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserTrainingWeekContent_Users");
        }
    }
}
