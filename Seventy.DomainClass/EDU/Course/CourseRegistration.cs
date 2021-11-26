using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seventy.DomainClass.Core;
using System;
using System.Collections.Generic;

namespace Seventy.DomainClass.EDU.Course
{
    public class CourseRegistration : CoreBase
    {
        public int UserID { get; set; }
        public int CourseID { get; set; }
        public int CourseGroupID { get; set; }
        public int TermID { get; set; }
        public string DocumentsState { get; set; }
        public string CertificateType { get; set; }
        public int? Progress { get; set; }
        public int? LikeRank { get; set; }
        public string AchievementsState { get; set; }
        public string HozoriState { get; set; }
        public int? CateringPackId { get; set; }
        public string ResidState { get; set; }

        public CateringPackage CateringPack { get; set; }
        public Course Course { get; set; }
        public CourseGroups CourseGroup { get; set; }
        public Users RegUser { get; set; }
        public Term.Term Term { get; set; }
        public Users User { get; set; }
    }
    public class CourseRegistrationConfiguration : IEntityTypeConfiguration<CourseRegistration>
    {
        public void Configure(EntityTypeBuilder<CourseRegistration> builder)
        {
            builder.ToTable("CourseRegistration", "EDU");

            builder.Property(e => e.CertificateType).HasMaxLength(50);

            builder.Property(e => e.DocumentsState).HasMaxLength(50);

            builder.Property(e => e.HozoriState).HasMaxLength(50);

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.ResidState).HasMaxLength(50);

            builder.HasOne(d => d.CateringPack)
                .WithMany(p => p.CourseRegistration)
                .HasForeignKey(d => d.CateringPackId)
                .HasConstraintName("FK_CourseRegistration_CateringPackage");

            builder.HasOne(d => d.CourseGroup)
                .WithMany(p => p.CourseRegistrations)
                .HasForeignKey(d => d.CourseGroupID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CourseGroup_Course");

            builder.HasOne(d => d.Course)
                .WithMany(p => p.CourseRegistration)
                .HasForeignKey(d => d.CourseID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Course_Course");

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.CourseRegistrationRegUser)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CourseRegistration_RegUserID_Users");

            builder.HasOne(d => d.Term)
                .WithMany(p => p.CourseRegistration)
                .HasForeignKey(d => d.TermID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CourseRegistration_Term");

            builder.HasOne(d => d.User)
                .WithMany(p => p.CourseRegistrationUser)
                .HasForeignKey(d => d.UserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CourseRegistration_Users");
        }
    }
}
