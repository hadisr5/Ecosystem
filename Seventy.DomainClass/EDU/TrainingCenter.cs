using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seventy.DomainClass.Core;

namespace Seventy.DomainClass.EDU
{
    public class TrainingCenter : Core.CoreBase
    {
        public string Title { get; set; }
        public int PlaceID { get; set; }

        public Places Place { get; set; }
        public Users RegUser { get; set; }
    }
    public class TrainingCenterConfiguration : IEntityTypeConfiguration<TrainingCenter>
    {
        public void Configure(EntityTypeBuilder<TrainingCenter> builder)
        {
            builder.ToTable("TrainingCenter", "EDU");

            builder.Property(e => e.Title)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.HasOne(d => d.Place)
                .WithMany(p => p.TrainingCenter)
                .HasForeignKey(d => d.PlaceID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TrainingCenter_Places");

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.TrainingCenter)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TrainingCenter_Users");
        }
    }
}
