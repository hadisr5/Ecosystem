using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Seventy.DomainClass.Core
{
    public class UserAccess : CoreBase
    {
        public int AccessID { get; set; }
        public int UserID { get; set; }

        public Users Users { get; set; }
        public Access Access { get; set; }
        public Users RegUser { get; set; }
    }
    public class UserAccessConfiguration : IEntityTypeConfiguration<UserAccess>
    {
        public void Configure(EntityTypeBuilder<UserAccess> builder)
        {
            builder.ToTable("UserAccess", "Core");

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.UserAccess)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(o => o.Users)
                .WithMany(m => m.UserAccessPermision)
                .HasForeignKey(f => f.UserID)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasOne(o => o.Access)
                .WithMany(m => m.UserAccesses)
                .HasForeignKey(f => f.AccessID)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}