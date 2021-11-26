using Seventy.Data;
using Seventy.DomainClass.EDU.TrainingWeek;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class TrainingWeekRepository : Repository<TrainingWeek>, ITrainingWeekRepository
    {
        public TrainingWeekRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
