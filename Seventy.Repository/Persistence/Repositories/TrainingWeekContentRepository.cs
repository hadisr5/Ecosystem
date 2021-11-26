using Seventy.Data;
using Seventy.DomainClass.EDU.TrainingWeek;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class TrainingWeekContentRepository : Repository<TrainingWeekContent>, ITrainingWeekContentRepository
    {
        public TrainingWeekContentRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
