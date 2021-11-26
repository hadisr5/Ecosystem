using Seventy.Data;
using Seventy.DomainClass.EDU.Exercise;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class ExerciseUserRepository : Repository<ExerciseUser>, IExerciseUserRepository
    {
        public ExerciseUserRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
