using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Seventy.DomainClass.Core
{
    public class DefaultRoleAccess : CoreBase
    {
        public int AccessID { get; set; }
        public int RoleID { get; set; }

        public Access Access { get; set; }
        public Roles Role { get; set; }
        public Users RegUser { get; set; }
    }
    public class DefaultRoleAccessConfiguration : IEntityTypeConfiguration<DefaultRoleAccess>
    {
        public void Configure(EntityTypeBuilder<DefaultRoleAccess> builder)
        {
            builder.ToTable("DefaultRoleAccess", "Core");

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.DefaultRoleAccessesReg)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(o => o.Access)
                .WithMany(m => m.DefaultRoleAccesses)
                .HasForeignKey(f => f.AccessID)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(o => o.Role)
                .WithMany(m => m.DefaultRoleAccesses)
                .HasForeignKey(f => f.RoleID)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
