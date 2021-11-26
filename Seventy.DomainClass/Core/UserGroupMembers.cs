using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Seventy.DomainClass.Core
{
    public class UserGroupMembers : CoreBase
    {
        public int UserID { get; set; }
        public int UserGroupID { get; set; }

        public Users RegUser { get; set; }
        public Users User { get; set; }
        public UserGroups UserGroup { get; set; }
    }
    public class UserGroupMembersConfiguration : IEntityTypeConfiguration<UserGroupMembers>
    {
        public void Configure(EntityTypeBuilder<UserGroupMembers> builder)
        {
            builder.ToTable("UserGroupMembers", "Core");

            builder.HasIndex(e => new { e.UserID, e.UserGroupID })
                .HasName("UK_UserGroupMembers")
                .IsUnique();

            builder.Property(e => e.RegDate).HasColumnType("datetime2(0)");

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.UserGroupMembersRegUser)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserGroupMembers_RegUserID_Users");

            builder.HasOne(d => d.UserGroup)
                .WithMany(p => p.UserGroupMembers)
                .HasForeignKey(d => d.UserGroupID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserGroupMembers_UserGroups");

            builder.HasOne(d => d.User)
                .WithMany(p => p.UserGroupMembersUser)
                .HasForeignKey(d => d.UserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserGroupMembers_Users");
        }
    }
}
