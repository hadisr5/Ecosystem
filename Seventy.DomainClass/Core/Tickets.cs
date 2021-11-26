using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Seventy.DomainClass.Core
{
    public class Tickets : CoreBase
    {
        public int UserID { get; set; }
        public string Title { get; set; }
        public string Section { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public string Actions { get; set; }
        public int ResponderUserID { get; set; }
        public string Response { get; set; }

        public Users RegUser { get; set; }
    }
    public class TicketsConfiguration : IEntityTypeConfiguration<Tickets>
    {
        public void Configure(EntityTypeBuilder<Tickets> builder)
        {
            builder.ToTable("Tickets", "Core");

            builder.Property(e => e.Actions).IsRequired();

            builder.Property(e => e.Description).IsRequired();

            builder.Property(e => e.Priority)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Response).IsRequired();

            builder.Property(e => e.Section)
                .IsRequired()
                .HasMaxLength(35);

            builder.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(12);

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(35);

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.Tickets)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tickets_RegUserID_Users");
        }
    }
}
