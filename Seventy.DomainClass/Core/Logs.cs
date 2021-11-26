using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Seventy.DomainClass.Core
{
    public class Logs : CoreBase
    {
        public int UserID { get; set; }
        public string Section { get; set; }
        public string LogType { get; set; }
        public string IP { get; set; }
        public string MAC { get; set; }

        public Users RegUser { get; set; }
    }
    public class LogsConfiguration : IEntityTypeConfiguration<Logs>
    {
        public void Configure(EntityTypeBuilder<Logs> builder)
        {
            builder.ToTable("Logs", "Core");

            builder.Property(e => e.Description).IsRequired();

            builder.Property(e => e.IP)
                .HasColumnName("IP")
                .HasMaxLength(15)
                .IsUnicode(false);

            builder.Property(e => e.LogType)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(e => e.MAC)
                .HasColumnName("MAC")
                .HasMaxLength(17)
                .IsUnicode(false);

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Section)
                .IsRequired()
                .HasMaxLength(35);

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.Logs)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Logs_RegUserID_Users");
        }
    }
}
