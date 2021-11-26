using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Seventy.DomainClass.EDU.Teacher
{
    public class TeacherEvalIndex : Core.CoreBase
    {
        public int TeacherID { get; set; }
        public string Category { get; set; }
    }
    public class TeacherEvalIndexConfiguration : IEntityTypeConfiguration<TeacherEvalIndex>
    {
        public void Configure(EntityTypeBuilder<TeacherEvalIndex> builder)
        {
            builder.ToTable("TeacherEvalIndex", "EDU");
        }
    }
}
