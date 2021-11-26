using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seventy.DomainClass.Core;

namespace Seventy.DomainClass.EDU.Poll
{
    public class PollUser : Core.CoreBase
    {
        public int PollID { get; set; }
        public int UserID { get; set; }
        public string Answer { get; set; }
        public string Result { get; set; }
        public int LikeRank { get; set; }

        public Poll Poll { get; set; }
        public Users RegUser { get; set; }
        public Users User { get; set; }
    }
    public class PollUserConfiguration : IEntityTypeConfiguration<PollUser>
    {
        public void Configure(EntityTypeBuilder<PollUser> builder)
        {
            builder.ToTable("PollUser", "EDU");

            builder.Property(e => e.Answer).IsRequired();

            builder.Property(e => e.RegDate).HasColumnType("datetime2(0)");

            builder.HasOne(d => d.Poll)
                .WithMany(p => p.PollUser)
                .HasForeignKey(d => d.PollID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PollUser_Poll");

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.PollUserRegUser)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PollUser_RegUserID_Users");

            builder.HasOne(d => d.User)
                .WithMany(p => p.PollUserUser)
                .HasForeignKey(d => d.UserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PollUser_Users");
        }
    }
}
