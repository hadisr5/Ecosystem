using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seventy.DomainClass.Core;
using Seventy.DomainClass.EDU.Course;
using System;
using System.Collections.Generic;

namespace Seventy.DomainClass.EDU
{
    public class CateringPackage : Core.CoreBase
    {

        public string Title { get; set; }
        public int Price { get; set; }

        public Users RegUser { get; set; }
        public ICollection<CourseRegistration> CourseRegistration { get; set; }
    }
    public class CateringPackageConfiguration : IEntityTypeConfiguration<CateringPackage>
    {
        public void Configure(EntityTypeBuilder<CateringPackage> builder)
        {
            builder.ToTable("CateringPackage", "EDU");

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.CateringPackage)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CateringPackage_Users");
        }
    }
}
