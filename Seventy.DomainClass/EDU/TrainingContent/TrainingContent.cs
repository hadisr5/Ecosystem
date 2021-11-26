using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seventy.DomainClass.Core;
using Seventy.DomainClass.EDU.TrainingWeek;
using System;
using System.Collections.Generic;

namespace Seventy.DomainClass.EDU.TrainingContent
{
    public class TrainingContent : Core.CoreBase
    {
        public string Title { get; set; }
        public string ContentType { get; set; }
        public int? ExternalContentID { get; set; }
        public int? FileID { get; set; }
        public string DemoState { get; set; }
        public string Achievement { get; set; }

        public Files File { get; set; }
        public Users RegUser { get; set; }
        public ICollection<ContentObservation> ContentObservation { get; set; }
        public ICollection<TrainingWeekContent> TrainingWeekContent { get; set; }
        public ICollection<UserContent> UserContent { get; set; }
        public ICollection<UserTrainingWeekContent> UserTrainingWeekContent { get; set; }
    }
    public class TrainingContentConfiguration : IEntityTypeConfiguration<TrainingContent>
    {
        public void Configure(EntityTypeBuilder<TrainingContent> builder)
        {
            builder.ToTable("TrainingContent", "EDU");

            builder.Property(e => e.ContentType)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.DemoState)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.RegDate).HasColumnType("datetime2(0)");

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(d => d.File)
                .WithMany(p => p.TrainingContent)
                .HasForeignKey(d => d.FileID)
                .HasConstraintName("FK_TrainingContent_Files");

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.TrainingContent)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TrainingContent_Users");
        }
    }
}
