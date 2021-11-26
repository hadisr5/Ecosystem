using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seventy.DomainClass.Core;
using System;
using System.Collections.Generic;

namespace Seventy.DomainClass.EDU.TrainingWeek
{
    public class TrainingWeekContent : Core.CoreBase
    {
        public string ContentType { get; set; }
        public Users RegUser { get; set; }

        public int ContentID { get; set; }
        public TrainingContent.TrainingContent Content { get; set; }

        public int TrainingWeekID { get; set; }
        public TrainingWeek TrainingWeek { get; set; }
    }
    public class TrainingWeekContentConfiguration : IEntityTypeConfiguration<TrainingWeekContent>
    {
        public void Configure(EntityTypeBuilder<TrainingWeekContent> builder)
        {
            builder.ToTable("TrainingWeekContent", "EDU");

            builder.Property(e => e.ContentType)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(d => d.Content)
                .WithMany(p => p.TrainingWeekContent)
                .HasForeignKey(d => d.ContentID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TrainingWeekContent_TrainingContent");

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.TrainingWeekContent)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TrainingWeekContent_Users");

            builder.HasOne(d => d.TrainingWeek)
                .WithMany(p => p.TrainingWeekContent)
                .HasForeignKey(d => d.TrainingWeekID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TrainingWeekContent_TrainingWeek");

        }
    }
}
