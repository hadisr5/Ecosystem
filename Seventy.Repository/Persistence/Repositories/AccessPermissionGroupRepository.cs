using Seventy.Data;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class AccessPermissionGroupRepository : Repository<DomainClass.Core.AccessPermissionGroup>, IAccessPermissionGroupRepository
    {
        public AccessPermissionGroupRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
