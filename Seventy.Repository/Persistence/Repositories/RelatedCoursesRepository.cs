using Seventy.Data;
using Seventy.DomainClass.EDU.Course;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class RelatedCoursesRepository : Repository<RelatedCourses>, IRelatedCoursesRepository
    {
        public RelatedCoursesRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
