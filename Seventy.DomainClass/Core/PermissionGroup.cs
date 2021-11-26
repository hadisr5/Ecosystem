using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seventy.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Seventy.DomainClass.Core
{
    public class PermissionGroup : CoreBase
    {
        public string Title { get; set; }

        public Users RegUser { get; set; }
        public ICollection<AccessPermissionGroup> AccessPermissionGroups { get; set; }
        public ICollection<UserPermissionGroup> UserPermissionGroups { get; set; }
    }
    public class AccessGroupConfiguration : IEntityTypeConfiguration<PermissionGroup>
    {
        public void Configure(EntityTypeBuilder<PermissionGroup> builder)
        {
            builder.ToTable("AccessGroup", "Core");

            builder.Property(p => p.Title).IsRequired(true);

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.AccessGroupReg)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
