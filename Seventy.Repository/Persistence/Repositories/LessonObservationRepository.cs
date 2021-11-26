using Seventy.Data;
using Seventy.DomainClass.EDU.Lesson;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class LessonObservationRepository : Repository<LessonObservation>, ILessonObservationRepository
    {
        public LessonObservationRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
