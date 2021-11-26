using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seventy.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Seventy.DomainClass.Core
{
    public class Access : CoreBase
    {
        public int AccessControl { get; set; }
        public eAccessType AccessType { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Route { get; set; }
        public int Index { get; set; }
        public bool AllowAnonymous { get; set; } = false;

        public Users RegUser { get; set; }
        public ICollection<UserAccess> UserAccesses { get; set; }
        public ICollection<DefaultRoleAccess> DefaultRoleAccesses { get; set; }
        public ICollection<AccessPermissionGroup> AccessPermissionGroups { get; set; }
    }
    public class AccessConfiguration : IEntityTypeConfiguration<Access>
    {
        public void Configure(EntityTypeBuilder<Access> builder)
        {
            builder.ToTable("Access", "Core");

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.Accesses)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
