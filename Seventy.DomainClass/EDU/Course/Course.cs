using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seventy.DomainClass.Core;
using Seventy.DomainClass.EDU.Term;
using Seventy.DomainClass.EDU.TrainingWeek;
using System;
using System.Collections.Generic;

namespace Seventy.DomainClass.EDU.Course
{
    public class Course : CoreBase
    {
        public string Title { get; set; }
        public string CourseType { get; set; }
        public string RequiredDocuments { get; set; }
        public int CategoryID { get; set; }
        public int Duration { get; set; }
        public string PublishState { get; set; }
        public string Achievements { get; set; }
        public string HozoriType { get; set; }
        public int Price { get; set; }
        public int? PhotoFileID { get; set; }

        public CourseCategory Category { get; set; }
        public Files PhotoFile { get; set; }
        public Users RegUser { get; set; }
        public ICollection<CourseGroups> CourseGroups { get; set; }
        public ICollection<CourseObservation> CourseObservation { get; set; }
        public ICollection<CourseRegistration> CourseRegistration { get; set; }
        public ICollection<FavoriteCourses> FavoriteCourses { get; set; }
        public ICollection<RelatedCourses> RelatedCoursesFirstCourse { get; set; }
        public ICollection<RelatedCourses> RelatedCoursesSecondCourse { get; set; }
        public ICollection<Term.Term> Terms { get; set; }
        public ICollection<UserTrainingWeekContent> UserTrainingWeekContent { get; set; }
        public ICollection<CertificateUser> CertificateUsers { get; set; }
        public ICollection<TermLesson> TermLessons { get; set; }
    }
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Course", "EDU");


            builder.Property(e => e.Achievements).IsRequired();

            builder.Property(e => e.CourseType)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.HozoriType)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.PublishState)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(d => d.Category)
                .WithMany(p => p.Course)
                .HasForeignKey(d => d.CategoryID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Course_CourseCategory");

            builder.HasOne(d => d.PhotoFile)
                .WithMany(p => p.Course)
                .HasForeignKey(d => d.PhotoFileID)
                .HasConstraintName("FK_Course_PhotoFileID_Files");

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.Course)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Course_Users");
        }
    }
}
