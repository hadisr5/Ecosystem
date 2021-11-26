using Seventy.Data;
using Seventy.DomainClass.Core;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class PermissionGroupRepository : Repository<PermissionGroup>, IPermissionGroupRepository
    {
        public PermissionGroupRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
