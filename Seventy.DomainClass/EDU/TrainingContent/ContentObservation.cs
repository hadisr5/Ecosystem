using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seventy.DomainClass.Core;
using System;
using System.Collections.Generic;

namespace Seventy.DomainClass.EDU.TrainingContent
{
    public class ContentObservation : Core.CoreBase
    {
        public int UserID { get; set; }
        public int ContentID { get; set; }

        public TrainingContent Content { get; set; }
        public Users RegUser { get; set; }
        public Users User { get; set; }
    }
    public class ContentObservationConfiguration : IEntityTypeConfiguration<ContentObservation>
    {
        public void Configure(EntityTypeBuilder<ContentObservation> builder)
        {
            builder.ToTable("ContentObservation", "EDU");

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.HasOne(d => d.Content)
                .WithMany(p => p.ContentObservation)
                .HasForeignKey(d => d.ContentID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ContentObservation_TrainingContent");

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.ContentObservationRegUser)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ContentObservation_RegUserID_Users");

            builder.HasOne(d => d.User)
                .WithMany(p => p.ContentObservationUser)
                .HasForeignKey(d => d.UserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ContentObservation_Users");
        }
    }
}
