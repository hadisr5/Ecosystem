using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace Seventy.DomainClass.Core
{
    public  class UserGroups : CoreBase
    {
        public string Title { get; set; }

        public Users RegUser { get; set; }
        public ICollection<UserGroupMembers> UserGroupMembers { get; set; }
    }
    public class UserGroupsConfiguration : IEntityTypeConfiguration<UserGroups>
    {
        public void Configure(EntityTypeBuilder<UserGroups> builder)
        {
            builder.ToTable("UserGroups", "Core");

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.UserGroups)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserGroups_Users");
        }
    }
}
