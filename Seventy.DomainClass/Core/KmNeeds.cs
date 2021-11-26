using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Seventy.DomainClass.Core
{
    public class KMNeeds : CoreBase
    {
        public int UserID { get; set; }
        public string Section { get; set; }
        public int CatID { get; set; }
        public string Response { get; set; }
        public DateTime ResponseDate { get; set; }

        public Users RegUser { get; set; }
    }
    public class KmNeedsConfiguration : IEntityTypeConfiguration<KMNeeds>
    {
        public void Configure(EntityTypeBuilder<KMNeeds> builder)
        {
            builder.ToTable("KmNeeds", "Core");

            builder.Property(e => e.Description).IsRequired();

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Response).IsRequired();

            builder.Property(e => e.ResponseDate).HasColumnType("datetime2(0)");

            builder.Property(e => e.Section)
                .IsRequired()
                .HasMaxLength(15)
                .IsUnicode(false);

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.KmNeeds)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_KmNeeds_RegUserID_Users");
        }
    }
}
