using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seventy.DomainClass.Core;
using System;
using System.Collections.Generic;

namespace Seventy.DomainClass.EDU.TrainingContent
{
    public class UserContent : Core.CoreBase
    {
        public int UserID { get; set; }
        public int TrainingContentID { get; set; }
        public int? Progress { get; set; }
        public int? LikeRank { get; set; }

        public Users RegUser { get; set; }
        public TrainingContent TrainingContent { get; set; }
        public Users User { get; set; }
    }
    public class UserContentConfiguration : IEntityTypeConfiguration<UserContent>
    {
        public void Configure(EntityTypeBuilder<UserContent> builder)
        {
            builder.ToTable("UserContent", "EDU");

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.UserContentRegUser)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserContent_RegUserID_Users");

            builder.HasOne(d => d.TrainingContent)
                .WithMany(p => p.UserContent)
                .HasForeignKey(d => d.TrainingContentID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserContent_TrainingContent");

            builder.HasOne(d => d.User)
                .WithMany(p => p.UserContentUser)
                .HasForeignKey(d => d.UserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserContent_Users");
        }
    }
}
