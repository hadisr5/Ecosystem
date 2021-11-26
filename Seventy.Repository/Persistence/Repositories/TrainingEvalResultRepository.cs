using Seventy.Data;
using Seventy.DomainClass.EDU.TrainingEval;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class TrainingEvalResultRepository : Repository<TrainingEvalResult>, ITrainingEvalResultRepository
    {
        public TrainingEvalResultRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
