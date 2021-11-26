using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seventy.DomainClass.Core;
using Seventy.DomainClass.EDU.Course;
using Seventy.DomainClass.EDU.TrainingWeek;
using System;
using System.Collections.Generic;

namespace Seventy.DomainClass.EDU.Term
{
    public class Term : Core.CoreBase
    {
        public string Title { get; set; }
        public int CourseID { get; set; }
        public int CourseGroupID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Duration { get; set; }

        public Course.Course Course { get; set; }
        public CourseGroups CourseGroup { get; set; }
        public Users RegUser { get; set; }
        public ICollection<CourseRegistration> CourseRegistration { get; set; }
        public ICollection<UserTrainingWeekContent> UserTrainingWeekContents { get; set; }
        public ICollection<TrainingWeek.TrainingWeek> TrainingWeeks { get; set; }
        public ICollection<TermLesson> TermLessons { get; set; }
    }
    public class TermConfiguration : IEntityTypeConfiguration<Term>
    {
        public void Configure(EntityTypeBuilder<Term> builder)
        {
            builder.ToTable("Term", "EDU");

            builder.Property(e => e.RegDate).HasColumnType("datetime2(0)");

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(d => d.CourseGroup)
                .WithMany(p => p.Term)
                .HasForeignKey(d => d.CourseGroupID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Term_CourseGroups");

            builder.HasOne(d => d.Course)
                .WithMany(p => p.Terms)
                .HasForeignKey(d => d.CourseID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Term_Course");

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.Term)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Term_Users");
        }
    }
}
