using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seventy.DomainClass.Core;
using System;
using System.Collections.Generic;

namespace Seventy.DomainClass.EDU
{
    public class Certificate : Core.CoreBase
    {
        public string Title { get; set; }
        public string Type { get; set; }
        public int SampleFileID { get; set; }
        public string CreditorOrganization { get; set; }
        public int Price { get; set; }

        public Users RegUser { get; set; }
        public Files SampleFile { get; set; }
        public ICollection<CertificateUser> CertificateUser { get; set; }
    }
    public class CertificateConfiguration : IEntityTypeConfiguration<Certificate>
    {
        public void Configure(EntityTypeBuilder<Certificate> builder)
        {
            builder.ToTable("Certificate", "EDU");

            builder.Property(e => e.CreditorOrganization)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Type)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.Certificate)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Certificate_Users");

            builder.HasOne(d => d.SampleFile)
                .WithMany(p => p.Certificate)
                .HasForeignKey(d => d.SampleFileID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Certificate_Files");
        }
    }
}
