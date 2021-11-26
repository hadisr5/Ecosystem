using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Seventy.DomainClass.Core
{
    public class Documents : CoreBase
    {
        public int UserID { get; set; }
        public string Section { get; set; }
        public string DocType { get; set; }
        public string DocFormat { get; set; }
        public string FilePath { get; set; }

        public Users RegUser { get; set; }
    }
    public class DocumentsConfiguration : IEntityTypeConfiguration<Documents>
    {
        public void Configure(EntityTypeBuilder<Documents> builder)
        {
            builder.ToTable("Documents", "Core");

            builder.Property(e => e.Description).IsRequired();

            builder.Property(e => e.DocFormat)
                .IsRequired()
                .HasMaxLength(4)
                .IsUnicode(false);

            builder.Property(e => e.DocType)
                .IsRequired()
                .HasMaxLength(32)
                .IsUnicode(false);

            builder.Property(e => e.FilePath).IsRequired();

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Section)
                .IsRequired()
                .HasMaxLength(64)
                .IsUnicode(false);

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.Documents)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Documents_RegUserID_Users");
        }
    }
}
