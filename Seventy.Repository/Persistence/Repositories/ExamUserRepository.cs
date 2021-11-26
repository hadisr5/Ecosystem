using Seventy.Data;
using Seventy.DomainClass.EDU.Exam;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class ExamUserRepository : Repository<ExamUser>, IExamUserRepository
    {
        public ExamUserRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
