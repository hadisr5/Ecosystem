using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Seventy.DomainClass.Core
{
    public class DocumentType : CoreBase
    {
        public string Title { get; set; }

        public Users RegUser { get; set; }
        public ICollection<UserDocuments> UserDocuments { get; set; }
    }
    public class DocumentTypeConfiguration : IEntityTypeConfiguration<DocumentType>
    {
        public void Configure(EntityTypeBuilder<DocumentType> builder)
        {
            builder.ToTable("DocumentType", "Core");

            builder.HasIndex(e => e.Title)
                   .HasName("UK_DocumentType")
                   .IsUnique();

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.DocumentType)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DocumentType_Users");
        }
    }
}
