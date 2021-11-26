using Seventy.Data;
using Seventy.DomainClass.EDU.TrainingWeek;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class UserTrainingWeekContentRepository : Repository<UserTrainingWeekContent>, IUserTrainingWeekContentRepository
    {
        public UserTrainingWeekContentRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
