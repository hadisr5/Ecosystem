using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seventy.DomainClass.Core;
using System;
using System.Collections.Generic;

namespace Seventy.DomainClass.Accounting
{
    public class TafsiliAccount : Core.CoreBase
    {
        public int MoeinID { get; set; }
        public string Title { get; set; }

        public MoeinAccount Moein { get; set; }
        public Users RegUser { get; set; }

        public ICollection<SettlementRequest> SettlementRequest { get; set; }
    }
    public class TafsiliAccountConfiguration : IEntityTypeConfiguration<TafsiliAccount>
    {
        public void Configure(EntityTypeBuilder<TafsiliAccount> builder)
        {
            builder.ToTable("TafsiliAccount", "Accounting");

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(d => d.Moein)
                .WithMany(p => p.TafsiliAccount)
                .HasForeignKey(d => d.MoeinID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TafsiliAccount_MoeinAccount");

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.TafsiliAccount)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TafsiliAccount_Users");
        }
    }
}
