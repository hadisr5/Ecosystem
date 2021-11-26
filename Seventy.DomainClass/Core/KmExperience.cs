using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Seventy.DomainClass.Core
{
    public class KMExperience : CoreBase
    {
        public int UserID { get; set; }
        public string Section { get; set; }
        public int CatID { get; set; }
        public int Priority { get; set; }

        public Users RegUser { get; set; }
    }
    public class KmExperienceConfiguration : IEntityTypeConfiguration<KMExperience>
    {
        public void Configure(EntityTypeBuilder<KMExperience> builder)
        {
            builder.ToTable("KmExperience", "Core");

            builder.Property(e => e.Description).IsRequired();

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Section)
                .IsRequired()
                .HasMaxLength(15)
                .IsUnicode(false);

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.KmExperience)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_KmExperience_RegUserID_Users");
        }
    }
}
