using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seventy.DomainClass.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Seventy.DomainClass.EDU.Course
{
    public  class CourseCategory : CoreBase
    {
        [Display(Name = "دسته اصلی")]
        public string PrimaryCat { get; set; }

        [Display(Name = "دسته فرعی")]
        public string SecondaryCat { get; set; }

        public  Users RegUser { get; set; }
        public  ICollection<Course> Course { get; set; }
    }
    public class CourseCategoryConfiguration : IEntityTypeConfiguration<CourseCategory>
    {
        public void Configure(EntityTypeBuilder<CourseCategory> builder)
        {
            builder.ToTable("CourseCategory", "EDU");

            builder.Property(e => e.PrimaryCat)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.SecondaryCat)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.CourseCategory)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CourseCategory_Users");
        }
    }
}
