using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Seventy.DomainClass.Core
{
    public class Tags : CoreBase
    {
        public int UserID { get; set; }
        public string TagContainer { get; set; }

        public Users RegUser { get; set; }
    }
    public class TagsConfiguration : IEntityTypeConfiguration<Tags>
    {
        public void Configure(EntityTypeBuilder<Tags> builder)
        {
            builder.ToTable("Tags", "Core");

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.TagContainer).IsRequired();

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.Tags)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tags_RegUserID_Users");
        }
    }
}
