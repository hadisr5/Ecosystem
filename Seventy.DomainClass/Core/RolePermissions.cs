using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Seventy.DomainClass.Core
{
    public class RolePermissions : CoreBase
    {
        public int RoleID { get; set; }
        public int PermissionID { get; set; }

        public Users RegUser { get; set; }
    }
    public class RolePermissionsConfiguration : IEntityTypeConfiguration<RolePermissions>
    {
        public void Configure(EntityTypeBuilder<RolePermissions> builder)
        {
            builder.ToTable("RolePermissions", "Core");

            builder.HasIndex(e => new { e.RoleID, e.PermissionID })
                    .HasName("UK_RolePermissions")
                    .IsUnique();

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RolePermissions_RegUserID_Users");
        }
    }
}
