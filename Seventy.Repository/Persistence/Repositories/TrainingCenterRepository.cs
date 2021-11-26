using Seventy.Data;
using Seventy.DomainClass.EDU;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class TrainingCenterRepository : Repository<TrainingCenter>, ITrainingCenterRepository
    {
        public TrainingCenterRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
