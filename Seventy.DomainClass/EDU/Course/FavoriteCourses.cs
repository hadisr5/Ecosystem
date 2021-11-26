using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seventy.DomainClass.Core;
using System;
using System.Collections.Generic;

namespace Seventy.DomainClass.EDU.Course
{
    public  class FavoriteCourses : CoreBase
    {
        public int UserID { get; set; }
        public int CourseID { get; set; }
        public int LikeRank { get; set; }

        public Course Course { get; set; }
        public Users RegUser { get; set; }
        public Users User { get; set; }
    }
    public class FavoriteCoursesConfiguration : IEntityTypeConfiguration<FavoriteCourses>
    {
        public void Configure(EntityTypeBuilder<FavoriteCourses> builder)
        {
            builder.ToTable("FavoriteCourses", "EDU");

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.HasOne(d => d.Course)
                .WithMany(p => p.FavoriteCourses)
                .HasForeignKey(d => d.CourseID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FavoriteCourses_Course");

            builder.HasOne(d => d.RegUser)
                .WithMany(p => p.FavoriteCoursesRegUser)
                .HasForeignKey(d => d.RegUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FavoriteCourses_RegUserID_Users");

            builder.HasOne(d => d.User)
                .WithMany(p => p.FavoriteCoursesUser)
                .HasForeignKey(d => d.UserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FavoriteCourses_Users");
        }
    }
}
