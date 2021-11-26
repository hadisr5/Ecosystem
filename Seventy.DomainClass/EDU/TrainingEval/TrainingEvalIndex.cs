using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seventy.DomainClass.Core;
using System;
using System.Collections.Generic;

namespace Seventy.DomainClass.EDU.TrainingEval
{
    public class TrainingEvalIndex : Core.CoreBase
    {
        public string Title { get; set; }
        public string TargetType { get; set; }
        public int TargetID { get; set; }

        public Users RegUser { get; set; }
        public ICollection<TrainingEvalResult> TrainingEvalResult { get; set; }
    }
    public class TrainingEvalIndexConfiguration : IEntityTypeConfiguration<TrainingEvalIndex>
    {
        public void Configure(EntityTypeBuilder<TrainingEvalIndex> builder)
        {
            builder.ToTable("TrainingEvalIndex", "EDU");

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.TargetType)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.TrainingEvalIndex)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TrainingEvalIndex_Users");
        }
    }
}
