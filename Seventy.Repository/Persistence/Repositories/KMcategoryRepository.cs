using Seventy.Data;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class KMcategoryRepository : Repository<DomainClass.Core.KMcategory>, IKMcategoryRepository
    {
        public KMcategoryRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
