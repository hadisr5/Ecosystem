using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Seventy.DomainClass.Core
{
    public class KMcategory : CoreBase
    {
        public string Title { get; set; }

        public Users RegUser { get; set; }
    }
    public class KMcategoryConfiguration : IEntityTypeConfiguration<KMcategory>
    {
        public void Configure(EntityTypeBuilder<KMcategory> builder)
        {
            builder.ToTable("KMcategory", "Core");

            builder.Property(e => e.Description).IsRequired();

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(15)
                .IsUnicode(false);

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.Kmcategory)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_KMcategory_RegUserID_Users");
        }
    }
}
