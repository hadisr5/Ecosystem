using Seventy.Data;
using Seventy.DomainClass.EDU.Exercise;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class ExerciseRepository : Repository<Exercise>, IExerciseRepository
    {
        public ExerciseRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
