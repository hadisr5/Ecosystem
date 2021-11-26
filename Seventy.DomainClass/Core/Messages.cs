using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Seventy.DomainClass.Core
{
    public class Messages : CoreBase
    {
        public int SenderUserID { get; set; }
        public int ReceiverUserID { get; set; }
        public string MsgTitle { get; set; }
        public string MsgType { get; set; }
        public int MsgViewed { get; set; }

        public Users ReceiverUser { get; set; }
        public Users RegUser { get; set; }
        public Users SenderUser { get; set; }
    }
    public class MessagesConfiguration : IEntityTypeConfiguration<Messages>
    {
        public void Configure(EntityTypeBuilder<Messages> builder)
        {
            builder.ToTable("Messages", "Core");

            builder.Property(e => e.Description).IsRequired();

            builder.Property(e => e.MsgTitle)
                .IsRequired()
                .HasMaxLength(64)
                .IsUnicode(false);

            builder.Property(e => e.MsgType)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.ReceiverUserID).HasColumnName("ReceiverUserID");

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.HasOne(d => d.ReceiverUser)
                .WithMany(p => p.MessagesReceiverUser)
                .HasForeignKey(d => d.ReceiverUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Messages_ReceiverUserID_Users");

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.MessagesRegUser)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Messages_RegUserID_Users");

            builder.HasOne(d => d.SenderUser)
                .WithMany(p => p.MessagesSenderUser)
                .HasForeignKey(d => d.SenderUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Messages_SenderUserID_Users");
        }
    }
}
