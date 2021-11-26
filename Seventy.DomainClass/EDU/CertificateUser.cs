using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seventy.DomainClass.Core;
using Seventy.DomainClass.EDU.Course;
using System;
using System.Collections.Generic;

namespace Seventy.DomainClass.EDU
{
    public class CertificateUser : Core.CoreBase
    {
        public int CertificateID { get; set; }
        public int UserID { get; set; }
        public int? Grade { get; set; }
        public int CourseID { get; set; }
        public int CourseGroupID { get; set; }


        public Course.Course Course { get; set; }
        public Course.CourseGroups CourseGroups { get; set; }
        public Certificate Certificate { get; set; }
        public Users RegUser { get; set; }
        public Users User { get; set; }
    }
    public class CertificateUserConfiguration : IEntityTypeConfiguration<CertificateUser>
    {
        public void Configure(EntityTypeBuilder<CertificateUser> builder)
        {
            builder.ToTable("CertificateUser", "EDU");

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.HasOne(d => d.Certificate)
                .WithMany(p => p.CertificateUser)
                .HasForeignKey(d => d.CertificateID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CertificateUser_Certificate");

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.CertificateUserRegUser)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CertificateUser_RegUserID_Users");

            builder.HasOne(d => d.User)
                .WithMany(p => p.CertificateUserUser)
                .HasForeignKey(d => d.UserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CertificateUser_Users");

            builder.HasOne(h => h.Course)
                .WithMany(m => m.CertificateUsers)
                .HasForeignKey(f => f.CourseID)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(h => h.CourseGroups)
                .WithMany(m => m.CertificateUsers)
                .HasForeignKey(f => f.CourseGroupID)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
