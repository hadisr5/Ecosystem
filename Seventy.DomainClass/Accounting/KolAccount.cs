using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seventy.DomainClass.Core;
using System;
using System.Collections.Generic;

namespace Seventy.DomainClass.Accounting
{
    public class KolAccount : Core.CoreBase
    {
        public int GoroohID { get; set; }
        public string Title { get; set; }

        public GoroohAccount Gorooh { get; set; }
        public Users RegUser { get; set; }

        public ICollection<MoeinAccount> MoeinAccount { get; set; }
    }
    public class KolAccountConfiguration : IEntityTypeConfiguration<KolAccount>
    {
        public void Configure(EntityTypeBuilder<KolAccount> builder)
        {
            builder.ToTable("KolAccount", "Accounting");

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(d => d.Gorooh)
                .WithMany(p => p.KolAccount)
                .HasForeignKey(d => d.GoroohID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_KolAccount_GoroohAccount");

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.KolAccount)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_KolAccount_Users");
        }
    }
}
