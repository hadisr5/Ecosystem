using Seventy.Data;
using Seventy.DomainClass.EDU.Course;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class RequestedCoursesRepository : Repository<RequestedCourses>, IRequestedCoursesRepository
    {
        public RequestedCoursesRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
