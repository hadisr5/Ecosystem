using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Seventy.DomainClass.EDU.Teacher
{
    public class TeacherEvalResult : Core.CoreBase
    {
        public int TeacherEvalIndexID { get; set; }
        public int UserID { get; set; }
        public string Result { get; set; }
    }
    public class TeacherEvalResultConfiguration : IEntityTypeConfiguration<TeacherEvalResult>
    {
        public void Configure(EntityTypeBuilder<TeacherEvalResult> builder)
        {
            builder.ToTable("TeacherEvalResult", "EDU");
        }
    }
}
