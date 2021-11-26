using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seventy.DomainClass.Core;
using System.ComponentModel.DataAnnotations;

namespace Seventy.DomainClass.EDU.Exam
{
    public class QuestionOptions : Core.CoreBase
    {
        public int QuestionId { get; set; }
        public string Title { get; set; }
        public int? FileID { get; set; }


        [Display(Name = "گزینه صحیح")]
        public bool IsCorrect { get; set; }

        public Files File { get; set; }
        public Questions Question { get; set; }
        public Users RegUser { get; set; }
    }
    public class QuestionOptionsConfiguration : IEntityTypeConfiguration<QuestionOptions>
    {
        public void Configure(EntityTypeBuilder<QuestionOptions> builder)
        {
            builder.ToTable("QuestionOptions", "EDU");

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(500);

            builder.HasOne(d => d.File)
                .WithMany(p => p.QuestionOptions)
                .HasForeignKey(d => d.FileID)
                .HasConstraintName("FK_QuestionOptions_Files");

            builder.HasOne(d => d.Question)
                .WithMany(p => p.QuestionOptions)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QuestionOptions_Questions");

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.QuestionOptions)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QuestionOptions_Users");
        }
    }
}
