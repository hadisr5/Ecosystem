using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace Seventy.DomainClass.Core
{
    public class PlaceLayers : CoreBase
    {
        public string Title { get; set; }

        public Users RegUser { get; set; }
        public ICollection<Places> Places { get; set; }
    }
    public class PlaceLayersConfiguration : IEntityTypeConfiguration<PlaceLayers>
    {
        public void Configure(EntityTypeBuilder<PlaceLayers> builder)
        {
            builder.ToTable("PlaceLayers", "Core");

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.PlaceLayers)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PlaceLayers_RegUserID_Users");
        }
    }
}
