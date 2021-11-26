using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seventy.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Seventy.DomainClass.Core
{
    public class AccessPermissionGroup : CoreBase
    {
        public int AccessID { get; set; }
        public int PermissionGroupID { get; set; }

        public Access Access { get; set; }
        public PermissionGroup PermissionGroup { get; set; }
        public Users RegUser { get; set; }
    }
    public class AccessPermissionGroupConfiguration : IEntityTypeConfiguration<AccessPermissionGroup>
    {
        public void Configure(EntityTypeBuilder<AccessPermissionGroup> builder)
        {
            builder.ToTable("AccessPermissionGroup", "Core");

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.AccessPermissionGroupReg)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(o => o.Access)
                .WithMany(m => m.AccessPermissionGroups)
                .HasForeignKey(f => f.AccessID)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.PermissionGroup)
                .WithMany(m => m.AccessPermissionGroups)
                .HasForeignKey(f => f.PermissionGroupID)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
