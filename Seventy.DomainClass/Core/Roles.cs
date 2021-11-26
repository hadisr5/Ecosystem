using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace Seventy.DomainClass.Core
{
    public class Roles : CoreBase
    {
        public string Title { get; set; }
        public int Priority { get; set; }

        public Users RegUser { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<DefaultRoleAccess> DefaultRoleAccesses { get; set; }
    }
    public class RolesConfiguration : IEntityTypeConfiguration<Roles>
    {
        public void Configure(EntityTypeBuilder<Roles> builder)
        {
            builder.ToTable("Roles", "Core");

            builder.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(35);

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.Roles)
                .HasForeignKey(d => d.RegUserID);
        }
    }
}
