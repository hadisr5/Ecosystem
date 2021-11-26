using Seventy.Data;
using Seventy.DomainClass.EDU.Teacher;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class TeacherEvalResultRepository : Repository<TeacherEvalResult>, ITeacherEvalResultRepository
    {
        public TeacherEvalResultRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
