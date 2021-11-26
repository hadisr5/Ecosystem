using Seventy.Data;
using Seventy.DomainClass.EDU.TrainingEval;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class TrainingEvalIndexRepository : Repository<TrainingEvalIndex>, ITrainingEvalIndexRepository
    {
        public TrainingEvalIndexRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
