using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seventy.DomainClass.Core;
using System;
using System.Collections.Generic;

namespace Seventy.DomainClass.Accounting
{
    public class GoroohAccount : Core.CoreBase
    {
        public string Title { get; set; }
        public Users RegUser { get; set; }

        public ICollection<KolAccount> KolAccount { get; set; }
    }
    public class GoroohAccountConfiguration : IEntityTypeConfiguration<GoroohAccount>
    {
        public void Configure(EntityTypeBuilder<GoroohAccount> builder)
        {
            builder.ToTable("GoroohAccount", "Accounting");

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.GoroohAccount)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GoroohAccount_Users");
        }
    }
}
