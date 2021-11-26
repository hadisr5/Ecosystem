using Seventy.Data;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class KMExperienceRepository : Repository<DomainClass.Core.KMExperience>, IKMExperienceRepository
    {
        public KMExperienceRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
