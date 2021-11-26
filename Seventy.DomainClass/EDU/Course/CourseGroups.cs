using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seventy.DomainClass.Core;
using Seventy.DomainClass.EDU.Term;
using Seventy.DomainClass.EDU.TrainingWeek;
using System;
using System.Collections.Generic;

namespace Seventy.DomainClass.EDU.Course
{
    public class CourseGroups : CoreBase
    {
        public int CourseID { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Capacity { get; set; }

        public Course Course { get; set; }
        public ICollection<Term.Term> Term { get; set; }
        public ICollection<UserTrainingWeekContent> UserTrainingWeekContent { get; set; }
        public ICollection<CertificateUser> CertificateUsers { get; set; }
        public ICollection<CourseRegistration> CourseRegistrations { get; set; }
        public ICollection<TermLesson> TermLessons { get; set; }
    }
    public class CourseGroupsConfiguration : IEntityTypeConfiguration<CourseGroups>
    {
        public void Configure(EntityTypeBuilder<CourseGroups> builder)
        {
            builder.ToTable("CourseGroups", "EDU");

            builder.Property(e => e.EndDate).HasColumnType("datetime2(0)");

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.StartDate).HasColumnType("datetime2(0)");

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(d => d.Course)
                .WithMany(p => p.CourseGroups)
                .HasForeignKey(d => d.CourseID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CourseGroups_Course");
        }
    }
}
