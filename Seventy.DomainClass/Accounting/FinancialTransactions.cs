using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seventy.DomainClass.Core;
using System;
using System.Collections.Generic;

namespace Seventy.DomainClass.Accounting
{
    public class FinancialTransactions : Core.CoreBase
    {
        public int UserID { get; set; }

        public long Amount { get; set; }


        public Users User { get; set; }
        public Users RegUser { get; set; }
    }
    public class FinancialTransactionsConfiguration : IEntityTypeConfiguration<FinancialTransactions>
    {
        public void Configure(EntityTypeBuilder<FinancialTransactions> builder)
        {
            builder.ToTable("FinancialTransactions", "Accounting");

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");


            builder.HasOne(d => d.User)
                .WithMany(p => p.FinancialTransactionsUser)
                .HasForeignKey(d => d.UserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FinancialTransactions_UserID_Users");

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.FinancialTransactionsRegUser)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FinancialTransactions_RegUserID_Users");
        }
    }
}
