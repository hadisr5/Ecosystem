using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seventy.Common.Enums;

namespace Seventy.DomainClass.Core
{
    public class MenuAccess : CoreBase
    {
        public int MenuCode { get; set; }
        public int AccessCode { get; set; }
        public string Route { get; set; }
        public eModule eModule { get; set; }
        public int Order { get; set; }

        public Users RegUser { get; set; }
    }
    public class MenuAccessConfiguration : IEntityTypeConfiguration<MenuAccess>
    {
        public void Configure(EntityTypeBuilder<MenuAccess> builder)
        {
            builder.ToTable("MenuAccess", "Core");

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.MenuAccessReg)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull);


        }
    }
}
