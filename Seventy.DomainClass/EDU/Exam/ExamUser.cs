using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seventy.DomainClass.Core;
using System;
using System.Collections.Generic;

namespace Seventy.DomainClass.EDU.Exam
{
    public class ExamUser : Core.CoreBase
    {
        public int ExamID { get; set; }
        public int UserID { get; set; }
        public DateTime? StartTime { get; set; }

        public double? Result { get; set; }
        public int? LikeRank { get; set; }

        public Seventy.DomainClass.EDU.Exam.Exam Exam { get; set; }
        public Users RegUser { get; set; }
        public Users User { get; set; }
    }
    public class ExamUserConfiguration : IEntityTypeConfiguration<ExamUser>
    {
        public void Configure(EntityTypeBuilder<ExamUser> builder)
        {
            builder.ToTable("ExamUser", "EDU");

            builder.HasOne(d => d.Exam)
                  .WithMany(p => p.ExamUser)
                  .HasForeignKey(d => d.ExamID)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_ExamUser_Exam");

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.ExamUserRegUser)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ExamUser_RegUserID_Users");

            builder.HasOne(d => d.User)
                .WithMany(p => p.ExamUserUser)
                .HasForeignKey(d => d.UserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ExamUser_Users");
        }
    }
}
