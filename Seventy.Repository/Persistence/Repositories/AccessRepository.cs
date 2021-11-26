using Seventy.Data;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class AccessRepository : Repository<DomainClass.Core.Access>, IAccessRepository
    {
        public AccessRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
