using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seventy.DomainClass.Core;
using System;
using System.Collections.Generic;

namespace Seventy.DomainClass.EDU.Poll
{
    public class Poll : Core.CoreBase
    {
        public int TrainingWeekID { get; set; }
        public int Barom { get; set; }
        public string CorrectAnswer { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Users RegUser { get; set; }
        public TrainingWeek.TrainingWeek TrainingWeek { get; set; }
        public ICollection<PollUser> PollUser { get; set; }
    }
    public class PollConfiguration : IEntityTypeConfiguration<Poll>
    {
        public void Configure(EntityTypeBuilder<Poll> builder)
        {
            builder.ToTable("Poll", "EDU");

            builder.Property(e => e.CorrectAnswer).IsRequired();

            builder.Property(e => e.Description).IsRequired();

            builder.Property(e => e.EndDate).HasColumnType("datetime2(0)");

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.StartDate).HasColumnType("datetime2(0)");

            builder.Property(e => e.Status).HasMaxLength(50);

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.Poll)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Poll_Users");

            builder.HasOne(d => d.TrainingWeek)
                .WithMany(p => p.Poll)
                .HasForeignKey(d => d.TrainingWeekID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Poll_TrainingWeek");
        }
    }
}
