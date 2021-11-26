using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seventy.DomainClass.Core;
using System;
using System.Collections.Generic;

namespace Seventy.DomainClass.EDU.Exercise
{
    public class ExerciseUser : Core.CoreBase
    {
        public int ExerciseId { get; set; }
        public int UserID { get; set; }
        public string Answer { get; set; }
        public int FileID { get; set; }
        public string Result { get; set; }
        public int? LikeRank { get; set; }

        public Exercise Exercise { get; set; }
        public Files File { get; set; }
        public Users RegUser { get; set; }
        public Users User { get; set; }
    }
    public class ExerciseUserConfiguration : IEntityTypeConfiguration<ExerciseUser>
    {
        public void Configure(EntityTypeBuilder<ExerciseUser> builder)
        {
            builder.ToTable("ExerciseUser", "EDU");

            builder.Property(e => e.Answer).IsRequired();

            builder.HasOne(d => d.Exercise)
                .WithMany(p => p.ExerciseUser)
                .HasForeignKey(d => d.ExerciseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ExerciseUser_Exercise");

            builder.HasOne(d => d.File)
                .WithMany(p => p.ExerciseUser)
                .HasForeignKey(d => d.FileID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ExerciseUser_Files");

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.ExerciseUserRegUser)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ExerciseUser_RegUserID_Users");

            builder.HasOne(d => d.User)
                .WithMany(p => p.ExerciseUserUser)
                .HasForeignKey(d => d.UserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ExerciseUser_Users");
        }
    }
}
