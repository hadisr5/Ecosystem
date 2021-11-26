using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seventy.DomainClass.Core;
using System;
using System.Collections.Generic;

namespace Seventy.DomainClass.Accounting
{
    public class SettlementRequest : Core.CoreBase
    {
        public int TafsiliID { get; set; }
        public string Title { get; set; }

        public long Amount { get; set; }

        public string Type { get; set; }

        public int UserID { get; set; }

        public string PaymentMethod { get; set; }
        public TafsiliAccount Tafsili { get; set; }
        public Users User { get; set; }
        public Users RegUser { get; set; }
    }
    public class SettlementRequestConfiguration : IEntityTypeConfiguration<SettlementRequest>
    {
        public void Configure(EntityTypeBuilder<SettlementRequest> builder)
        {
            builder.ToTable("SettlementRequest", "Accounting");

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Type)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.PaymentMethod)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(d => d.Tafsili)
                .WithMany(p => p.SettlementRequest)
                .HasForeignKey(d => d.TafsiliID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SettlementRequest_TafsiliAccount");

            builder.HasOne(d => d.User)
                .WithMany(p => p.SettlementRequestUser)
                .HasForeignKey(d => d.UserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SettlementRequest_UserID_Users");

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.SettlementRequestRegUser)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SettlementRequest_RegUserID_Users");
        }
    }
}
