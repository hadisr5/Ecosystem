using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seventy.DomainClass.EDU;
using System.Collections.Generic;

namespace Seventy.DomainClass.Core
{
    public class Places : CoreBase
    {
        public int? LayerID { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public PlaceLayers Layer { get; set; }
        public Users RegUser { get; set; }
        public ICollection<TrainingCenter> TrainingCenter { get; set; }
    }
    public class PlacesConfiguration : IEntityTypeConfiguration<Places>
    {
        public void Configure(EntityTypeBuilder<Places> builder)
        {
            builder.ToTable("Places", "Core");

            builder.Property(e => e.ID)
                .ValueGeneratedNever();

            builder.Property(e => e.Address).IsRequired();

            builder.Property(e => e.Latitude).HasColumnType("decimal(12, 9)");

            builder.Property(e => e.Longitude).HasColumnType("decimal(12, 9)");

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(d => d.Layer)
                .WithMany(p => p.Places)
                .HasForeignKey(d => d.LayerID)
                .HasConstraintName("FK_Places_LocationLayers");

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.Places)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Places_Users");
        }
    }
}
