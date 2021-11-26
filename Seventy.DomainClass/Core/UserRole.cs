using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Seventy.DomainClass.Core
{
    public class UserRole : CoreBase
    {
        public int UserID { get; set; }
        public int RoleID { get; set; }

        public Users User { get; set; }
        public Roles Role { get; set; }
        public Users RegUser { get; set; }
    }
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRole", "Core");

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.UserRolesReg)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(d => d.User)
                .WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserID)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.Role)
                .WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
