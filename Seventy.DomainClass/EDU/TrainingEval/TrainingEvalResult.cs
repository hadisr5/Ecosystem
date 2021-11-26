using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seventy.DomainClass.Core;
using System;
using System.Collections.Generic;

namespace Seventy.DomainClass.EDU.TrainingEval
{
    public class TrainingEvalResult : Core.CoreBase
    {
        public int TrainingEvalIndexID { get; set; }
        public int UserID { get; set; }
        public int Result { get; set; }

        public Users RegUser { get; set; }
        public TrainingEvalIndex TrainingEvalIndex { get; set; }
        public Users User { get; set; }
    }
    public class TrainingEvalResultConfiguration : IEntityTypeConfiguration<TrainingEvalResult>
    {
        public void Configure(EntityTypeBuilder<TrainingEvalResult> builder)
        {
            builder.ToTable("TrainingEvalResult", "EDU");

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.TrainingEvalResultRegUser)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TrainingEvalResult_RegUserID_Users");

            builder.HasOne(d => d.TrainingEvalIndex)
                .WithMany(p => p.TrainingEvalResult)
                .HasForeignKey(d => d.TrainingEvalIndexID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TrainingEvalResult_TrainingEvalIndex");

            builder.HasOne(d => d.User)
                .WithMany(p => p.TrainingEvalResultUser)
                .HasForeignKey(d => d.UserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TrainingEvalResult_Users");
        }
    }
}
