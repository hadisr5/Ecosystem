using Seventy.Data;
using Seventy.DomainClass.EDU.Lesson;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class LessonRepository : Repository<Lesson>, ILessonRepository
    {
        public LessonRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
