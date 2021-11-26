using Seventy.Data;
using Seventy.DomainClass.EDU.Lesson;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class UserLessonRepository : Repository<UserLesson>, IUserLessonRepository
    {
        public UserLessonRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
