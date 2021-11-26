using Seventy.Data;
using Seventy.DomainClass.EDU.Teacher;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class TeacherEvalIndexRepository : Repository<TeacherEvalIndex>, ITeacherEvalIndexRepository
    {
        public TeacherEvalIndexRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
