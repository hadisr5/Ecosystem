using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seventy.DomainClass.Core;
using System;
using System.Collections.Generic;

namespace Seventy.DomainClass.Accounting
{
    public class MoeinAccount : Core.CoreBase
    {
        public int KolID { get; set; }
        public string Title { get; set; }

        public KolAccount Kol { get; set; }
        public Users RegUser { get; set; }

        public ICollection<TafsiliAccount> TafsiliAccount { get; set; }
    }
    public class MoeinAccountConfiguration : IEntityTypeConfiguration<MoeinAccount>
    {
        public void Configure(EntityTypeBuilder<MoeinAccount> builder)
        {
            builder.ToTable("MoeinAccount", "Accounting");

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(d => d.Kol)
                .WithMany(p => p.MoeinAccount)
                .HasForeignKey(d => d.KolID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MoeinAccount_KolAccount");

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.MoeinAccount)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MoeinAccount_Users");
        }
    }
}
