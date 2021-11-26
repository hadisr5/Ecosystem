using Seventy.Data;
using Seventy.DomainClass.EDU.Course;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class CourseObservationRepository : Repository<CourseObservation>, ICourseObservationRepository
    {
        public CourseObservationRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
