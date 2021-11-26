using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seventy.DomainClass.Core;
using System;
using System.Collections.Generic;

namespace Seventy.DomainClass.EDU
{
    public class LMS : Core.CoreBase
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Users RegUser { get; set; }
    }
    public class LMSConfiguration : IEntityTypeConfiguration<LMS>
    {
        public void Configure(EntityTypeBuilder<LMS> builder)
        {
            builder.ToTable("LMS", "EDU");

            builder.Property(e => e.EndDate).HasColumnType("datetime2(0)");

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.StartDate).HasColumnType("datetime2(0)");

            builder.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Url)
                .IsRequired()
                .HasColumnName("URL");

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.Lms)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LMS_Users");
        }
    }
}
