using Seventy.Data;
using Seventy.DomainClass.EDU.Term;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class TermLessonRepository : Repository<TermLesson>, ITermLessonRepository
    {
        public TermLessonRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
