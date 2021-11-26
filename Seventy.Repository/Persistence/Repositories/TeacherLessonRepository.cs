using Seventy.Data;
using Seventy.DomainClass.EDU.Teacher;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class TeacherLessonRepository : Repository<TeacherLesson>, ITeacherLessonRepository
    {
        public TeacherLessonRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
