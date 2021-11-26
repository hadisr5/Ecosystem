using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Seventy.DomainClass.Core
{
    public  class UserDocuments : CoreBase
    {
        public int UserID { get; set; }
        public int DocumentTypeID { get; set; }
        public int FileID { get; set; }

        public DocumentType DocumentType { get; set; }
        public Files File { get; set; }
        public Users RegUser { get; set; }
        public Users User { get; set; }
    }
    public class UserDocumentsConfiguration : IEntityTypeConfiguration<UserDocuments>
    {
        public void Configure(EntityTypeBuilder<UserDocuments> builder)
        {
            builder.ToTable("UserDocuments", "Core");

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.HasOne(d => d.DocumentType)
                .WithMany(p => p.UserDocuments)
                .HasForeignKey(d => d.DocumentTypeID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserDocuments_DocumentType");

            builder.HasOne(d => d.File)
                .WithMany(p => p.UserDocuments)
                .HasForeignKey(d => d.FileID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserDocuments_Files");

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.UserDocumentsRegUser)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserDocuments_RegUserID_Users");

            builder.HasOne(d => d.User)
                .WithMany(p => p.UserDocumentsUser)
                .HasForeignKey(d => d.UserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserDocuments_Users");
        }
    }
}
