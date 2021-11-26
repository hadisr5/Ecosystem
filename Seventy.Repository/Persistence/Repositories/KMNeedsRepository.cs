using Seventy.Data;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class KMNeedsRepository : Repository<DomainClass.Core.KMNeeds>, IKMNeedsRepository
    {
        public KMNeedsRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
