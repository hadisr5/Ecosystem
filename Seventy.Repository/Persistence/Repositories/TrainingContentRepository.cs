using Seventy.Data;
using Seventy.DomainClass.EDU.TrainingContent;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class TrainingContentRepository : Repository<TrainingContent>, ITrainingContentRepository
    {
        public TrainingContentRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
