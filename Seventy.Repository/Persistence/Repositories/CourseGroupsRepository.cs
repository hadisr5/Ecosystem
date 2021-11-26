using Seventy.Data;
using Seventy.DomainClass.EDU.Course;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class CourseGroupsRepository : Repository<CourseGroups>, ICourseGroupsRepository
    {
        public CourseGroupsRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
