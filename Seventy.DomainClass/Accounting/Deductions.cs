using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seventy.DomainClass.Core;
using System;
using System.Collections.Generic;

namespace Seventy.DomainClass.Accounting
{
    public class Deductions : Core.CoreBase
    {
        public string Title { get; set; }

        public Users RegUser { get; set; }
    }
    public class DeductionsConfiguration : IEntityTypeConfiguration<Deductions>
    {
        public void Configure(EntityTypeBuilder<Deductions> builder)
        {
            builder.ToTable("Deductions", "Accounting");

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.Deductions)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Deductions_Users");
        }
    }
}
