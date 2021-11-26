using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace Seventy.DomainClass.Core
{
    public class UserPermissionGroup : CoreBase
    {
        public int UserID { get; set; }
        public int PermissionGroupID { get; set; }

        public Users User { get; set; }
        public PermissionGroup PermissionGroup { get; set; }
        public Users RegUser { get; set; }
        public ICollection<AccessPermissionGroup> AccessPermissionGroups { get; set; }
    }
    public class UserPermissionGroupConfiguration : IEntityTypeConfiguration<UserPermissionGroup>
    {
        public void Configure(EntityTypeBuilder<UserPermissionGroup> builder)
        {
            builder.ToTable("UserPermissionGroup", "Core");

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.UserPermissionGroupReg)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(o => o.User)
               .WithMany(m => m.UserPermissionGroup)
               .HasForeignKey(f => f.UserID)
               .IsRequired(true)
               .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.PermissionGroup)
               .WithMany(m => m.UserPermissionGroups)
               .HasForeignKey(f => f.PermissionGroupID)
               .IsRequired(true)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
