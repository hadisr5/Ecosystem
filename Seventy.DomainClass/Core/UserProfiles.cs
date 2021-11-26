using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Seventy.DomainClass.Core
{
    public class UserProfiles : CoreBase
    {
        public int? UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Father { get; set; }
        public string Sex { get; set; }
        public string Tavalod { get; set; }
        public string CodeMelli { get; set; }
        public string Country { get; set; }
        public string Ostan { get; set; }
        public string Shahr { get; set; }
        public string OstanSokoonat { get; set; }
        public string ShahrSokoonat { get; set; }
        public string Address { get; set; }
        public string Tel { get; set; }
        public string Cell { get; set; }
        public string Madrak { get; set; }
        public string Reshte { get; set; }
        public string Daneshgah { get; set; }
        public int? PhotoFileId { get; set; }
        public string FullName => $"{FirstName} {LastName}";

        public Files PhotoFile { get; set; }
        public Users RegUser { get; set; }
        public Users User { get; set; }
    }
    public class UserProfilesConfiguration : IEntityTypeConfiguration<UserProfiles>
    {
        public void Configure(EntityTypeBuilder<UserProfiles> builder)
        {
            builder.ToTable("UserProfiles", "Core");

            builder.Property(e => e.Address).IsRequired();

            builder.Property(e => e.Cell)
                .IsRequired()
                .HasMaxLength(12);

            builder.Property(e => e.CodeMelli)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false);

            builder.Property(e => e.Country)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(e => e.Daneshgah)
                .IsRequired()
                .HasMaxLength(25);

            builder.Property(e => e.Father)
                .IsRequired()
                .HasMaxLength(25);

            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(e => e.Madrak)
                .IsRequired()
                .HasMaxLength(12);

            builder.Property(e => e.Ostan)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(e => e.OstanSokoonat)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Reshte)
                .IsRequired()
                .HasMaxLength(25);

            builder.Property(e => e.Sex)
                .IsRequired()
                .HasMaxLength(3);

            builder.Property(e => e.Shahr)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(e => e.ShahrSokoonat)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(e => e.Tavalod)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false);

            builder.Property(e => e.Tel)
                .IsRequired()
                .HasMaxLength(12);

            builder.HasOne(d => d.PhotoFile)
                .WithMany(p => p.UserProfiles)
                .HasForeignKey(d => d.PhotoFileId)
                .HasConstraintName("FK_UserProfiles_Files");

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.UserProfilesRegUser)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserProfiles_RegUserID_Users");

            builder.HasOne(d => d.User)
                .WithOne(p => p.UserProfile)
                .HasForeignKey<UserProfiles>(d => d.UserID)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_UserProfiles_Users");
        }
    }
}
