using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Seventy.DomainClass.Core
{
    public class Permissions : CoreBase
    {
        public string Title { get; set; }
        public string Entitle { get; set; }
        public string Section { get; set; }

        public Users RegUser { get; set; }
    }
    public class PermissionsConfiguration : IEntityTypeConfiguration<Permissions>
    {
        public void Configure(EntityTypeBuilder<Permissions> builder)
        {
            builder.ToTable("Permissions", "Core");

            builder.Property(e => e.Entitle)
                .IsRequired()
                .HasColumnName("ENTitle")
                .HasMaxLength(35);

            builder.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Section)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(35);

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.Permissions)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Permissions_RegUserID_Users");
        }
    }
}
