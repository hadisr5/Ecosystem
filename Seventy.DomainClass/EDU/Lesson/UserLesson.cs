using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Seventy.DomainClass.EDU.Lesson
{
    public class UserLesson : Core.CoreBase
    {
        public int UserId { get; set; }
        public int LessonId { get; set; }
        public string Status { get; set; }
        public int? LikeRank { get; set; }
    }
    public class UserLessonConfiguration : IEntityTypeConfiguration<UserLesson>
    {
        public void Configure(EntityTypeBuilder<UserLesson> builder)
        {
            builder.ToTable("UserLesson", "EDU");

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
